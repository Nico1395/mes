using Mes.Library.SignalR.Connections;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Mes.Library.ShopfloorCommands.Hub;

/// <summary>
/// Default implementation of <see cref="IShopfloorCommandHubController"/> that manages
/// command sending and broadcasting through the shopfloor command hub.
/// </summary>
/// <remarks>
/// This class is internal and is automatically registered with the DI container when
/// <see cref="CommandHubServiceCollectionExtensions.AddShopfloorCommandHub"/> is called.
/// <para>
/// The controller uses the <see cref="IHubContext{THub}"/> to send commands through
/// the SignalR hub infrastructure. It wraps all operations in try-catch blocks to
/// ensure that exceptions are logged and converted to <see cref="ShopfloorCommandResponse.Failure"/>
/// responses rather than propagating to callers.
/// </para>
/// <note type="warning">
/// There appears to be an issue in the SendAsync method implementation - it sends to All clients
/// instead of to a specific client. This should be addressed in a future update.
/// </note>
/// </remarks>
/// <seealso cref="IShopfloorCommandHubController"/>
/// <seealso cref="ShopfloorCommandHub"/>
internal sealed class ShopfloorCommandHubController(
    ILogger<ShopfloorCommandHubController> logger,
    ISignalRConnectionManager connectionManager,
    IHubContext<ShopfloorCommandHub> hubContext) : IShopfloorCommandHubController
{
    /// <summary>
    /// Asynchronously sends a command to a specific shopfloor through the hub.
    /// <para>
    /// This method uses the hub context to invoke the SendCommandV1 method on the hub,
    /// which will route the command to the appropriate shopfloor based on the
    /// <see cref="IShopfloorCommand.ReceiverShopfloorKey"/>.
    /// </para>
    /// </summary>
    /// <param name="command">The command to send.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result indicates whether
    /// the command was successfully sent (<see cref="ShopfloorCommandResponse.Success"/>) or
    /// if an error occurred (<see cref="ShopfloorCommandResponse.Failure"/>).
    /// </returns>
    public async Task<ShopfloorCommandResponse> SendAsync(IShopfloorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var connectionIds = await connectionManager.GetConnectionIdsAsync(
                ShopfloorCommandHub.Key2ConnectionPrefix,
                command.ReceiverShopfloorKey,
                cancellationToken);

            if (connectionIds.Length == 0)
                return ShopfloorCommandResponse.Failure;

            await hubContext.Clients.Client(connectionIds[0]).SendAsync(ShopfloorCommandConstants.V1.Receiver.ReceiveCommand, command, cancellationToken);
            return ShopfloorCommandResponse.Success;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sending a shopfloor command threw an exception.");
            return ShopfloorCommandResponse.Failure;
        }
    }

    /// <summary>
    /// Asynchronously broadcasts a command to all connected shopfloors through the hub.
    /// <para>
    /// This method uses the hub context to invoke the BroadcastCommandV1 method on the hub,
    /// which will send the command to all currently connected clients.
    /// </para>
    /// </summary>
    /// <param name="command">The command to broadcast to all connected shopfloors.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result indicates whether
    /// the command was successfully broadcast (<see cref="ShopfloorCommandResponse.Success"/>) or
    /// if an error occurred (<see cref="ShopfloorCommandResponse.Failure"/>).
    /// </returns>
    public async Task<ShopfloorCommandResponse> BroadcastAsync(IShopfloorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await hubContext.Clients.All.SendAsync(
                ShopfloorCommandConstants.V1.Hub.BroadcastCommand,
                command,
                cancellationToken);

            return ShopfloorCommandResponse.Success;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Broadcasting a shopfloor command threw an exception.");
            return ShopfloorCommandResponse.Failure;
        }
    }
}