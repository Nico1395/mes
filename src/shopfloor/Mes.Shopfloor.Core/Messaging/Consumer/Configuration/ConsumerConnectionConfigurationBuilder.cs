using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Consumer.Configuration;

public sealed class ConsumerConnectionConfigurationBuilder
{
    private readonly ConsumerConnectionConfiguration _configuration = new();

    public ConsumerConnectionConfigurationBuilder AddListeningChannel(string exchange, string queue, Action<ConsumerListeningChannelConfigurationBuilder> builderAction)
    {
        var builder = new ConsumerListeningChannelConfigurationBuilder(exchange, queue);
        builderAction(builder);

        _configuration.ChannelsInternal.Add(builder.Build());
        return this;
    }

    public ConsumerConnectionConfigurationBuilder ConfigureFactory(Action<ConnectionFactory> factoryAction)
    {
        factoryAction(_configuration.ConnectionFactory);
        return this;
    }
    
    internal ConsumerConnectionConfiguration Build() => _configuration;
}