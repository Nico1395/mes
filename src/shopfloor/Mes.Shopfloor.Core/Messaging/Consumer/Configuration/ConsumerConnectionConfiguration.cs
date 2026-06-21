using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Consumer.Configuration;

public sealed class ConsumerConnectionConfiguration
{
    internal List<ConsumerListeningChannelConfiguration> ChannelsInternal { get; } = [];

    public IReadOnlyList<ConsumerListeningChannelConfiguration> Channels => ChannelsInternal;
    public ConnectionFactory ConnectionFactory { get; set; } = new();
}
