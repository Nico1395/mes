using RabbitMQ.Client;

namespace Mes.Libraries.RabbitMQ.Connections;

public interface IConnectionProvider
{
    Task<IConnection> GetAsync(CancellationToken cancellationToken);
}