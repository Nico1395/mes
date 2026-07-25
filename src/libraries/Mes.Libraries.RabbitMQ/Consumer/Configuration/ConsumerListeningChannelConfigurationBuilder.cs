namespace Mes.Libraries.RabbitMQ.Consumer.Configuration;

public sealed class ConsumerListeningChannelConfigurationBuilder(string exchange, string queue)
{
    private readonly ConsumerListeningChannelConfiguration _configuration = new(exchange, queue);

    public ConsumerListeningChannelConfigurationBuilder WithRoutingKey(string routingKey)
    {
        _configuration.RoutingKeysInternal.Add(routingKey);
        return this;
    }

    public ConsumerListeningChannelConfigurationBuilder SetRequeueOnException(bool requeueOnException = true)
    {
        _configuration.RequeueOnException = requeueOnException;
        return this;
    }

    public ConsumerListeningChannelConfigurationBuilder SetPrefetchCount(ushort prefetchCount)
    {
        _configuration.PrefetchCount = prefetchCount;
        return this;
    }

    public ConsumerListeningChannelConfigurationBuilder ConfigureQueue(Action<DeclareQueueOptions> optionsAction)
    {
        optionsAction(_configuration.QueueOptions);
        return this;
    }
    
    internal ConsumerListeningChannelConfiguration Build() => _configuration;
}