using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR.Client;

namespace Mes.Library.ShopfloorCommands.Connection;

internal sealed class ShopfloorCommandHubConnectionHubConnectionProvider(IShopfloorCommandHubConnectionFactory connectionFactory) : IShopfloorCommandHubConnectionProvider, IAsyncDisposable
{
    private static readonly ConcurrentDictionary<string, HubConnection> Connections = new();

    public async ValueTask DisposeAsync()
    {
        foreach (var connection in Connections.Values)
            await connection.DisposeAsync();
    }

    public async Task<HubConnection> GetAsync(string key, CancellationToken cancellationToken)
    {
        if (Connections.TryGetValue(key, out var connection))
        {
            return connection;
        }

        connection = await connectionFactory.CreateV1Async(cancellationToken);
        return Connections[key] = connection;
    }

    public void Remove(string key)
    {
        Connections.TryRemove(key, out _);
    }
}