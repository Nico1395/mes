using Mes.Shopfloor.Core.Messaging.Consumer.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Mes.Shopfloor.Core.Messaging.Connections;

internal sealed class ConnectionProvider(
    ILogger<ConnectionProvider> _logger,
    ConsumerConnectionConfiguration _connectionConfiguration) : IConnectionProvider, IAsyncDisposable, IDisposable
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
        catch (Exception e)
        {
            _logger.LogError(e, "Failed creating a connection.");
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