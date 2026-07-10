using RabbitMQ.Client;

namespace Mes.Shopfloor.Shared.SharedKernel.Messaging.Connections;

public sealed class RabbitMQConnectionConfiguration
{
    internal ConnectionFactory ConnectionFactoryInternal { get; } = new();
    internal List<AmqpTcpEndpoint> NodesInternal { get; set; } = [];

    public IConnectionFactory ConnectionFactory => ConnectionFactoryInternal;
    public IReadOnlyList<AmqpTcpEndpoint> Nodes => NodesInternal;
}