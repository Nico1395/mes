using System.Reflection;

namespace Mes.Libraries.RabbitMQ.Consumer.Configuration;

public sealed class RabbitMQConsumerConfigurationBuilder
{
    private readonly RabbitMQConsumerConfiguration _configuration = new();

    public RabbitMQConsumerConfigurationBuilder AddListeningChannel(string exchange, string queue, Action<ConsumerListeningChannelConfigurationBuilder> builderAction)
    {
        var builder = new ConsumerListeningChannelConfigurationBuilder(exchange, queue);
        builderAction(builder);

        _configuration.ChannelsInternal.Add(builder.Build());
        return this;
    }

    public RabbitMQConsumerConfigurationBuilder ScanInAssemblies(params Assembly[] assemblies)
    {
        _configuration.AssembliesInternal = assemblies.ToList();
        return this;
    }
    
    internal RabbitMQConsumerConfiguration Build() => _configuration;
}