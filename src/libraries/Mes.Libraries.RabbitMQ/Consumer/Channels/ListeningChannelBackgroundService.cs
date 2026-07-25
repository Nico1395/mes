using System.Text;
using Mes.Libraries.RabbitMQ.Connections;
using Mes.Libraries.RabbitMQ.Consumer.Configuration;
using Mes.Libraries.RabbitMQ.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Mes.Libraries.RabbitMQ.Consumer.Channels;

internal sealed class ListeningChannelBackgroundService(
    ILogger<ListeningChannelBackgroundService> _logger,
    IServiceProvider _serviceProvider,
    RabbitMQConsumerConfiguration _configuration,
    IConnectionProvider _connectionProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var connection = await _connectionProvider.GetAsync(stoppingToken);

            // Initialize a channel and queue for every configured listening channel.
            foreach (var channelConfiguration in _configuration.Channels)
            {
                var listeningChannel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
                await InitializeListeningChannelAsync(listeningChannel, channelConfiguration, stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Opening a connection and initializing listening channels threw an exception.");
            throw;
        }
    }

    private async Task InitializeListeningChannelAsync(IChannel channel, ConsumerListeningChannelConfiguration configuration, CancellationToken cancellationToken)
    {
        var ackLock = new SemaphoreSlim(1, 1);

        // Declare the exchange
        await channel.ExchangeDeclareAsync(
            exchange: configuration.Exchange,
            type: ExchangeType.Topic,
            durable: true,
            autoDelete: false,
            cancellationToken: cancellationToken);
        
        configuration.QueueOptions.Arguments ??= new Dictionary<string, object?>();
        configuration.QueueOptions.Arguments.TryAdd("x-queue-type", "quorum");

        // Declare the queue
        await channel.QueueDeclareAsync(
            queue: configuration.Queue,
            durable: configuration.QueueOptions.Durable,
            exclusive: configuration.QueueOptions.Exclusive,
            autoDelete: configuration.QueueOptions.AutoDelete,
            arguments: configuration.QueueOptions.Arguments,
            noWait: configuration.QueueOptions.NoWait,
            cancellationToken: cancellationToken);

        // Limit how many messages are accepted if the queue piles up (prefect count)
        await channel.BasicQosAsync(
            prefetchSize: 0,
            prefetchCount: configuration.PrefetchCount,
            global: false,
            cancellationToken);
        
        // Adjust the queue so it is getting routed to for the given routing keys
        foreach (var routingKey in configuration.RoutingKeys)
        {
            await channel.QueueBindAsync(
                queue: configuration.Queue,
                exchange: configuration.Exchange,
                routingKey: routingKey,
                arguments: configuration.QueueOptions.Arguments,
                noWait: configuration.QueueOptions.NoWait,
                cancellationToken: cancellationToken);
        }

        // Attach to an event that handles receiving a message
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (_, args) => ReceiveAsync(args, ackLock, channel, configuration, cancellationToken);
        
        // Configure consumption on that queue to the consumer (with the receive-event) and disable automatic acknowledging, since were doing that manually
        await channel.BasicConsumeAsync(
            queue: configuration.Queue,
            autoAck: false,
            consumer: consumer,
            cancellationToken: cancellationToken);
    }

    private async Task ReceiveAsync(BasicDeliverEventArgs args, SemaphoreSlim ackLock, IChannel channel, ConsumerListeningChannelConfiguration configuration, CancellationToken cancellationToken)
    {
        ConsumerResultCode code;

        try
        {
            // Fetch message type
            if (!MessageTypeResolver.TryResolveType(args.BasicProperties.Type, out var messageType))
                throw new InvalidOperationException("Failed to resolve message type.");

            // Fetch the consumption handle method
            if (!ConsumerMethodResolver.TryReflectMethod(messageType, out var handleAsync))
                throw new InvalidOperationException($"Failed to reflect method '{nameof(IConsumer<>.HandleAsync)}' from '{typeof(IConsumer<>).MakeGenericType(messageType)}'.");

            // Read the message body as a string
            var json = Encoding.UTF8.GetString(args.Body.Span);
            if (messageType == null || string.IsNullOrWhiteSpace(json))
                throw new InvalidOperationException("Failed to deserialize message.");

            // Execute deserialization and consume in a fresh service scope
            var message = MessageSerializer.Deserialize(messageType, json);
            using var scope = _serviceProvider.CreateScope();
            {
                ConsumerResult? combinedResult = null;

                var consumerType = typeof(IConsumer<>).MakeGenericType(messageType);
                var consumers = scope.ServiceProvider.GetServices(consumerType);
                foreach (var consumer in consumers)
                {
                    if (handleAsync.Invoke(consumer, parameters: [message, cancellationToken]) is not Task<ConsumerResult> task)
                        throw new InvalidOperationException($"Failed to handle '{messageType}'.");
                
                    var currentResult = await task;
                    combinedResult = combinedResult == null ? currentResult : combinedResult.Combine(currentResult);
                }

                // If the combined result is null, no consumptions even existed. We assume the message wasn't needed and can be acked.
                code = combinedResult?.Code ?? ConsumerResultCode.Ack;
            }
        }
        catch (Exception ex)
        {
            code = configuration.RequeueOnException ? ConsumerResultCode.NackRequeue : ConsumerResultCode.Nack;
            _logger.LogError(ex, "Receiving a message threw an exception.");
        }

        // Using a lock so the channel can be used safely in multiple threads
        // Intentionally avoiding the cancellation token, because when stopping the ack or nack should still go through to avoid message noise in the queue.
        await ackLock.WaitAsync(cancellationToken: CancellationToken.None);
            
        try
        {
            // Evaluate and act accordingly
            if (code == ConsumerResultCode.Ack)
            {
                await channel.BasicAckAsync(args.DeliveryTag, multiple: false, cancellationToken);
            }
            else
            {
                var requeue = code == ConsumerResultCode.NackRequeue;
                await channel.BasicNackAsync(args.DeliveryTag, multiple: false, requeue, cancellationToken);
            }
        }
        finally
        {
            ackLock.Release();
        }
    }
}
