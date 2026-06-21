using System.Collections.Concurrent;
using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Consumer.Configuration;

public sealed class ConsumerConnectionConfiguration
{
    internal List<ConsumerListeningChannelConfiguration> ChannelsInternal { get; } = [];
    internal ConnectionFactory ConnectionFactoryInternal { get; } = new();

    public IReadOnlyList<ConsumerListeningChannelConfiguration> Channels => ChannelsInternal;
    public IConnectionFactory ConnectionFactory => ConnectionFactoryInternal;
}
