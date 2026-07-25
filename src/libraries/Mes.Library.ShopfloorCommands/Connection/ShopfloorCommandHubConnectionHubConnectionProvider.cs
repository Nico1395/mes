using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR.Client;

namespace Mes.Library.ShopfloorCommands.Connection;

/// <summary>
/// Default implementation of <see cref="IShopfloorCommandHubConnectionProvider"/> that manages
/// a pool of SignalR hub connections.
/// <para>
/// This provider maintains a thread-safe cache of hub connections, allowing multiple
/// components to share the same connection instance and avoiding the overhead of
/// creating multiple connections to the same hub.
/// </para>
/// </summary>
/// <remarks>
/// This class is internal and is automatically registered with the DI container when
/// <see cref="CommandHubConnectionServiceCollectionExtensions.AddShopfloorCommandHubConnection"/>
/// is called.
/// <para>
/// The provider uses a static <see cref="ConcurrentDictionary{TKey,TValue}"/> to store connections,
/// ensuring thread-safe access from multiple concurrent consumers. Connections are created
/// lazily on first request and reused for subsequent requests with the same key.
/// </para>
/// <para>
/// When disposed, this provider will dispose all cached connections to clean up resources.
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandHubConnectionProvider"/>
/// <seealso cref="IShopfloorCommandHubConnectionFactory"/>
internal sealed class ShopfloorCommandHubConnectionHubConnectionProvider(IShopfloorCommandHubConnectionFactory connectionFactory) : IShopfloorCommandHubConnectionProvider, IAsyncDisposable
{
    /// <summary>
    /// Thread-safe dictionary that caches hub connections by their key.
    /// </summary>
    /// <remarks>
    /// This dictionary is static, meaning connections are shared across all instances
    /// of this provider within the same application domain.
    /// </remarks>
    private static readonly ConcurrentDictionary<string, HubConnection> Connections = new();

    /// <summary>
    /// Asynchronously disposes all cached hub connections.
    /// <para>
    /// This method is called when the provider is disposed, typically during application
    /// shutdown. It ensures all SignalR connections are properly cleaned up.
    /// </para>
    /// </summary>
    /// <returns>A value task that represents the asynchronous dispose operation.</returns>
    public async ValueTask DisposeAsync()
    {
        foreach (var connection in Connections.Values)
            await connection.DisposeAsync();
    }

    /// <summary>
    /// Asynchronously gets or creates a hub connection for the specified key.
    /// <para>
    /// If a connection for the given key already exists in the cache, it is returned immediately.
    /// Otherwise, a new connection is created using the factory and added to the cache.
    /// </para>
    /// </summary>
    /// <param name="key">The key identifying the connection to retrieve or create.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hub connection.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the connection cannot be created.</exception>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via the cancellation token.</exception>
    public async Task<HubConnection> GetAsync(string key, CancellationToken cancellationToken)
    {
        if (Connections.TryGetValue(key, out var connection))
        {
            return connection;
        }

        connection = await connectionFactory.CreateV1Async(cancellationToken);
        return Connections[key] = connection;
    }

    /// <summary>
    /// Removes and disposes the connection associated with the specified key.
    /// <para>
    /// This method removes the connection from the cache and allows it to be garbage
    /// collected. The connection will be disposed when it is no longer referenced elsewhere.
    /// </para>
    /// </summary>
    /// <param name="key">The key identifying the connection to remove.</param>
    /// <remarks>
    /// This method is safe to call even if no connection exists for the given key.
    /// The removed connection will be disposed when the last reference to it is released.
    /// </remarks>
    public void Remove(string key)
    {
        Connections.TryRemove(key, out _);
    }
}