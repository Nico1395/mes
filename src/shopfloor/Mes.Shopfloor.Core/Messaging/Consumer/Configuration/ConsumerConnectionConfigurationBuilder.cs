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

    public ConsumerConnectionConfigurationBuilder ConnectToCluster(string userName, string password, TimeSpan? recoveryInterval, IEnumerable<string> nodes)
    {
        _configuration.ConnectionFactoryInternal.UserName = userName;
        _configuration.ConnectionFactoryInternal.Password = password;
        _configuration.ConnectionFactoryInternal.AutomaticRecoveryEnabled = true;
        _configuration.ConnectionFactoryInternal.TopologyRecoveryEnabled = true;
        _configuration.ConnectionFactoryInternal.NetworkRecoveryInterval = recoveryInterval ?? TimeSpan.FromSeconds(5);

        _configuration.NodesInternal = nodes.Select(n =>
        {
            var parts = n.Split(':', 2);
            return new AmqpTcpEndpoint(parts[0], int.Parse(parts[1]));
        }).ToList();

        return this;
    }
    
    public ConsumerConnectionConfigurationBuilder ConnectToCluster(string userName, string password, IEnumerable<string> nodes)
    {
        return ConnectToCluster(userName, password, recoveryInterval: null, nodes);
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