using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Mes.Library.RabbitMQ.Producer;
using Mes.Library.ShopfloorCommands.Connection;
using Microsoft.AspNetCore.SignalR;

namespace Mes.Library.ShopfloorCommands.Hub;

/// <summary>
/// SignalR hub that manages shopfloor command communication.
/// <para>
/// This hub provides the central point for shopfloor-to-shopfloor communication using
/// SignalR for real-time messaging and RabbitMQ for persistent message publishing.
/// </para>
/// </summary>
/// <remarks>
/// This class is internal and is automatically mapped to the "/cmd/" route when
/// <see cref="WebApplicationExtensions.MapShopfloorCommandHub"/> is called.
/// <para>
/// The hub maintains a mapping of shopfloor keys to SignalR connection IDs, enabling
/// direct routing of commands to specific shopfloors. It also supports broadcasting
/// commands to all connected shopfloors.
/// </para>
/// <para>
/// For cross-shopfloor communication that requires persistence or guaranteed delivery,
/// commands can be forwarded to the RabbitMQ message bus.
/// </para>
/// </remarks>
/// <seealso cref="WebApplicationExtensions.MapShopfloorCommandHub"/>
/// <seealso cref="IShopfloorCommandHubController"/>
internal sealed class ShopfloorCommandHub(IMessagePublisher messagePublisher) : Microsoft.AspNetCore.SignalR.Hub
{
    /// <summary>
    /// Thread-safe dictionary that maps shopfloor keys to SignalR connection IDs.
    /// <para>
    /// This dictionary maintains the current connections for all registered shopfloors,
    /// enabling the hub to route commands to specific shopfloors by their key.
    /// </para>
    /// </summary>
    private static ConcurrentDictionary<string, string> ShopfloorConnections { get; } = [];

    /// <summary>
    /// Registers a shopfloor with its SignalR connection ID.
    /// <para>
    /// This method is called by shopfloors when they first connect to the hub.
    /// It associates the shopfloor's unique key with its current SignalR connection ID.
    /// </para>
    /// </summary>
    /// <param name="shopfloorKey">The unique key identifying the shopfloor to register.</param>
    /// <returns>A completed task.</returns>
    [HubMethodName(ShopfloorCommandConstants.V1.Hub.RegisterShopfloor)]
    public Task RegisterShopfloorV1(string shopfloorKey)
    {
        ShopfloorConnections[shopfloorKey] = Context.ConnectionId;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Sends a command to a specific shopfloor.
    /// <para>
    /// This method routes the command to the shopfloor identified by
    /// <see cref="IShopfloorCommand.ReceiverShopfloorKey"/>. If the target shopfloor
    /// is not currently connected, an exception is thrown.
    /// </para>
    /// </summary>
    /// <param name="command">The command to send. Must have a valid <see cref="IShopfloorCommand.ReceiverShopfloorKey"/>.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    /// <exception cref="Exception">
    /// Thrown if the target shopfloor (identified by <see cref="IShopfloorCommand.ReceiverShopfloorKey"/>) 
    /// is not currently connected.
    /// </exception>
    [HubMethodName(ShopfloorCommandConstants.V1.Hub.SendCommand)]
    public async Task SendCommandV1(IShopfloorCommand command)
    {
        if (!TryGetConnectionId(command.ReceiverShopfloorKey, out var connectionId))
            throw new Exception($"Shopfloor mit Key {command.ReceiverShopfloorKey} nicht verbunden.");

        await Clients.Client(connectionId).SendAsync(ShopfloorCommandConstants.V1.Receiver.ReceiveCommand, command);
    }

    /// <summary>
    /// Broadcasts a command to all connected shopfloors.
    /// <para>
    /// This method sends the specified command to all currently connected clients.
    /// It can be used for commands that need to be received by all shopfloors simultaneously.
    /// </para>
    /// </summary>
    /// <param name="command">The command to broadcast to all connected shopfloors.</param>
    /// <returns>A task that represents the asynchronous broadcast operation.</returns>
    [HubMethodName(ShopfloorCommandConstants.V1.Hub.BroadcastCommand)]
    public async Task BroadcastCommandV1(IShopfloorCommand command)
    {
        await Clients.All.SendAsync(ShopfloorCommandConstants.V1.Receiver.ReceiveCommand, command);
    }

    /// <summary>
    /// Forwards a command to the RabbitMQ message bus for persistent delivery.
    /// <para>
    /// This method publishes <see cref="IShopfloorToShopfloorCommand"/> instances to the
    /// RabbitMQ message bus, enabling cross-shopfloor communication with guaranteed delivery
    /// even if the target shopfloor is not currently connected via SignalR.
    /// </para>
    /// </summary>
    /// <param name="command">The command to forward. Must implement <see cref="IShopfloorToShopfloorCommand"/>.</param>
    /// <returns>A task that represents the asynchronous publish operation.</returns>
    /// <remarks>
    /// This method uses the <see cref="IMessagePublisher"/> to publish the command as a message.
    /// The publication will use the connection's abort token to ensure cleanup on disconnect.
    /// </remarks>
    [HubMethodName(ShopfloorCommandConstants.V1.Hub.Forward)]
    public Task ForwardV1(IShopfloorToShopfloorCommand command)
    {
        return messagePublisher.PublishAsync(command, Context.ConnectionAborted);
    }

    /// <summary>
    /// Attempts to retrieve the SignalR connection ID for a shopfloor key.
    /// </summary>
    /// <param name="shopfloorKey">The shopfloor key to look up.</param>
    /// <param name="connectionId">When this method returns, contains the connection ID if found; otherwise, null.</param>
    /// <returns>True if the shopfloor key was found and has an active connection; otherwise, false.</returns>
    private static bool TryGetConnectionId(string shopfloorKey, [NotNullWhen(true)] out string? connectionId)
    {
        return ShopfloorConnections.TryGetValue(shopfloorKey, out connectionId);
    }
}