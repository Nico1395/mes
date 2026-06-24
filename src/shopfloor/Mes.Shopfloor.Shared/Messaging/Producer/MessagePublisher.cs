using System.Text;
using Mes.Shopfloor.Shared.Messaging.Connections;
using Mes.Shopfloor.Shared.Messaging.Serialization;
using RabbitMQ.Client;

namespace Mes.Shopfloor.Shared.Messaging.Producer;

internal sealed class MessagePublisher(IConnectionProvider _connectionProvider) : IMessagePublisher
{
    private IChannel? _channel;

    public async Task PublishAsync(IMessage message, CancellationToken cancellationToken)
    {
        var messageType = message.GetType();
        var messageRoutes = MessageRouteResolver.ResolveRoutes(messageType);
        if (messageRoutes.Length == 0)
            throw new InvalidOperationException($"No routes for message of type '{messageType}' configured.");

        var json = MessageSerializer.Serialize(message);
        var properties = new BasicProperties()
        {
            Type = messageType.GetIdentifiableName(),
            MessageId = message.Id.ToString(),
            Timestamp = new AmqpTimestamp(message.OccurredAtUtc.Ticks)
        };
        var channel = await GetChannelAsync(cancellationToken);

        foreach (var messageRoute in messageRoutes)
        {
            await channel.BasicPublishAsync(
                exchange: "terminals",
                routingKey: messageRoute,
                mandatory: true,
                basicProperties: properties,
                body: Encoding.UTF8.GetBytes(json),
                cancellationToken: cancellationToken);
        }
    }

    private async Task<IChannel> GetChannelAsync(CancellationToken cancellationToken)
    {
        if (_channel != null)
            return _channel;

        var connection = await _connectionProvider.GetAsync(cancellationToken);
        return _channel = await connection.CreateChannelAsync(options: null, cancellationToken);
    }
}