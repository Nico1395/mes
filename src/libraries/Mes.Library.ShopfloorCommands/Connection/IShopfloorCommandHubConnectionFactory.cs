using Microsoft.AspNetCore.SignalR.Client;

namespace Mes.Library.ShopfloorCommands.Connection;

/// <summary>
/// Factory interface for creating SignalR hub connections to the shopfloor command hub.
/// <para>
/// This interface abstracts the creation of hub connections, allowing for different
/// connection strategies and configurations.
/// </para>
/// </summary>
/// <remarks>
/// Implementations of this interface are responsible for establishing SignalR connections
/// to the command hub with appropriate configuration (URL, protocol, reconnection settings, etc.)
/// and performing initial registration of the shopfloor.
/// <para>
/// The created <see cref="HubConnection"/> instances are used by both the sender and receiver
/// components to communicate with the command hub.
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandHubConnectionFactory"/>
/// <seealso cref="IShopfloorCommandHubConnectionProvider"/>
public interface IShopfloorCommandHubConnectionFactory
{
    /// <summary>
    /// Asynchronously creates a new SignalR hub connection to version 1 of the shopfloor command hub.
    /// <para>
    /// This method establishes the connection, starts it, and registers the shopfloor
    /// with the hub before returning the connection instance.
    /// </para>
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the connected hub connection.</returns>
    /// <exception cref="InvalidOperationException">Thrown if required configuration is missing.</exception>
    /// <exception cref="HubException">Thrown if the connection or registration fails.</exception>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via the cancellation token.</exception>
    Task<HubConnection> CreateV1Async(CancellationToken cancellationToken);
}