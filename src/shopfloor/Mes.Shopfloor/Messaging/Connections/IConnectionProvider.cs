using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Connections;

public interface IConnectionProvider
{
    Task<IConnection> GetAsync(CancellationToken cancellationToken);
}