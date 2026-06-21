using System.Reflection;
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
        factoryAction(_configuration.ConnectionFactoryInternal);
        return this;
    }

    public ConsumerConnectionConfigurationBuilder ScanInAssemblies(params Assembly[] assemblies)
    {
        _configuration.AssembliesInternal = assemblies.ToList();
        return this;
    }
    
    internal ConsumerConnectionConfiguration Build() => _configuration;
}