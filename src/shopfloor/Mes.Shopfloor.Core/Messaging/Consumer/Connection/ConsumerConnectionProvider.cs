using Mes.Shopfloor.Core.Messaging.Consumer.Configuration;
using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Consumer.Connection;

internal sealed class ConsumerConnectionProvider(IConnectionFactory _connectionFactory) : IConsumerConnectionProvider, IAsyncDisposable, IDisposable
{
    private IConnection? _connection;
    
    public async Task<IConnection> GetAsync(CancellationToken cancellationToken)
    {
        if (_connection != null)
            return _connection;

        return _connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection != null)
            await _connection.DisposeAsync();
    }
}