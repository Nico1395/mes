using RabbitMQ.Client;

namespace Mes.Library.RabbitMQ.Connections;

public interface IConnectionProvider
{
    Task<IConnection> GetAsync(CancellationToken cancellationToken);
}