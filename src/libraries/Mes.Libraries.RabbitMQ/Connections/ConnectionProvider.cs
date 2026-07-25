using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Mes.Libraries.RabbitMQ.Connections;

internal sealed class ConnectionProvider(
    ILogger<ConnectionProvider> _logger,
    RabbitMQConnectionConfiguration _connectionConfiguration) : IConnectionProvider, IAsyncDisposable, IDisposable
{
    private IConnection? _connection;

    public async Task<IConnection> GetAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_connection is { IsOpen: true })
                return _connection;

            return _connection = await _connectionConfiguration.ConnectionFactory.CreateConnectionAsync(
                _connectionConfiguration.Nodes,
                cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed creating a connection.");
            throw;
        }
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