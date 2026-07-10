using RabbitMQ.Client;

namespace Mes.Shopfloor.Shared.SharedKernel.Messaging.Connections;

public interface IConnectionProvider
{
    Task<IConnection> GetAsync(CancellationToken cancellationToken);
}