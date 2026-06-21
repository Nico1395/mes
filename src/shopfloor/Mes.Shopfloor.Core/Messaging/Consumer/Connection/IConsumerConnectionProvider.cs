using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Consumer.Connection;

public interface IConsumerConnectionProvider
{
    Task<IConnection> GetAsync(CancellationToken cancellationToken);
}