using System.Reflection;
using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Consumer.Configuration;

public sealed class ConsumerConnectionConfiguration
{
    internal List<ConsumerListeningChannelConfiguration> ChannelsInternal { get; } = [];
    internal ConnectionFactory ConnectionFactoryInternal { get; } = new();
    internal List<Assembly> AssembliesInternal { get; set; } = [];
    internal List<AmqpTcpEndpoint> NodesInternal { get; set; } = [];

    public IReadOnlyList<ConsumerListeningChannelConfiguration> Channels => ChannelsInternal;
    public IConnectionFactory ConnectionFactory => ConnectionFactoryInternal;
    public IReadOnlyList<Assembly> Assemblies => AssembliesInternal;
    public IReadOnlyList<AmqpTcpEndpoint> Nodes => NodesInternal;
}
