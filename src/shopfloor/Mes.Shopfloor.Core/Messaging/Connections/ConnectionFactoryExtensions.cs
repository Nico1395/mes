using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Connections;

public static class ConnectionFactoryExtensions
{
    public static void UseClustering(this ConnectionFactory factory, TimeSpan recoveryInterval, params string[] hostNames)
    {
        // TODO -> The factory is not utilizing its endpoints!
        factory.HostName = string.Empty;
        factory.Port = 0;
        factory.AutomaticRecoveryEnabled = true;
        factory.TopologyRecoveryEnabled = true;
        factory.NetworkRecoveryInterval = recoveryInterval;
        factory.EndpointResolverFactory = endpoints =>
        {
            endpoints = endpoints.Concat(hostNames.Select(hostName => new AmqpTcpEndpoint(hostName)));
            return new DefaultEndpointResolver(endpoints);
        };
    }
    
    public static void UseClustering(this ConnectionFactory factory, params string[] hostNames)
    {
        factory.UseClustering(TimeSpan.FromSeconds(5), hostNames);
    }
}