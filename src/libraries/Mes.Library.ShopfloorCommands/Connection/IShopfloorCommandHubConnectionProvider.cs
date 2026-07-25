using Microsoft.AspNetCore.SignalR.Client;

namespace Mes.Library.ShopfloorCommands.Connection;

/// <summary>
/// Provider interface for managing SignalR hub connections to the shopfloor command hub.
/// <para>
/// This interface abstracts connection pooling and management, allowing for reuse of
/// existing connections and cleanup when connections are no longer needed.
/// </para>
/// </summary>
/// <remarks>
/// Implementations of this interface typically maintain a pool or cache of hub connections,
/// keyed by some identifier (e.g., "hub" for the main command hub connection). This allows
/// multiple components to share the same connection instance, reducing overhead.
/// <para>
/// The provider is responsible for creating new connections when needed (via the factory)
/// and managing their lifecycle.
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandHubConnectionFactory"/>
/// <seealso cref="ShopfloorCommandHubConnectionHubConnectionProvider"/>
public interface IShopfloorCommandHubConnectionProvider
{
    /// <summary>
    /// Asynchronously gets or creates a hub connection for the specified key.
    /// <para>
    /// If a connection for the given key already exists, it is returned. Otherwise,
    /// a new connection is created using the factory and stored for future use.
    /// </para>
    /// </summary>
    /// <param name="key">The key identifying the connection to retrieve or create.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hub connection.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the connection cannot be established.</exception>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via the cancellation token.</exception>
    Task<HubConnection> GetAsync(string key, CancellationToken cancellationToken);

    /// <summary>
    /// Removes and disposes the connection associated with the specified key.
    /// <para>
    /// This method should be called when a connection is no longer needed to free resources.
    /// The connection will be properly disposed to release underlying resources.
    /// </para>
    /// </summary>
    /// <param name="key">The key identifying the connection to remove.</param>
    /// <remarks>
    /// This method is safe to call even if no connection exists for the given key.
    /// </remarks>
    void Remove(string key);
}