using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Producer.Channels;

public interface IChannelProvider
{
    Task<IChannel> GetAsync();
}