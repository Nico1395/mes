using RabbitMQ.Client;

namespace Mes.Shopfloor.Shared.Messaging.Connections;

public interface IConnectionProvider
{
    Task<IConnection> GetAsync(CancellationToken cancellationToken);
}