using RabbitMQ.Client;

namespace Mes.Library.RabbitMQ.Connections;

public sealed class RabbitMQConnectionConfigurationBuilder
{
    private readonly RabbitMQConnectionConfiguration _configuration = new();

    public RabbitMQConnectionConfigurationBuilder ConnectToCluster(string userName, string password, TimeSpan? recoveryInterval, IEnumerable<string> nodes)
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
    
    public RabbitMQConnectionConfigurationBuilder ConnectToCluster(string userName, string password, IEnumerable<string> nodes)
    {
        return ConnectToCluster(userName, password, recoveryInterval: null, nodes);
    }

    public RabbitMQConnectionConfigurationBuilder ConfigureFactory(Action<ConnectionFactory> factoryAction)
    {
        factoryAction(_configuration.ConnectionFactoryInternal);
        return this;
    }

    internal RabbitMQConnectionConfiguration Build() => _configuration;
}