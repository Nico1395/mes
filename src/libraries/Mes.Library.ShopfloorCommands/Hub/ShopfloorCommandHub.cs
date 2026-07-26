using Mes.Library.RabbitMQ.Producer;
using Mes.Library.SignalR.Connections;
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
internal sealed class ShopfloorCommandHub(
    ISignalRConnectionManager connectionManager,
    IMessagePublisher messagePublisher) : Microsoft.AspNetCore.SignalR.Hub
{
    public const string Key2ConnectionPrefix = "edge:signalr:shopfloor-commands";

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
        return connectionManager.AddConnectionIdAsync(
            Key2ConnectionPrefix,
            shopfloorKey,
            Context.ConnectionId,
            Context.ConnectionAborted);
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

    /// <inheritdoc/>
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return connectionManager.DeleteConnectionIdAsync(
            Key2ConnectionPrefix,
            Context.ConnectionId,
            Context.ConnectionAborted);
    }
}