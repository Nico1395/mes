using System.Reflection;

namespace Mes.Library.RabbitMQ.Consumer.Configuration;

public sealed class RabbitMQConsumerConfiguration
{
    internal List<ConsumerListeningChannelConfiguration> ChannelsInternal { get; } = [];
    internal List<Assembly> AssembliesInternal { get; set; } = [];

    public IReadOnlyList<ConsumerListeningChannelConfiguration> Channels => ChannelsInternal;
    public IReadOnlyList<Assembly> Assemblies => AssembliesInternal;
}
