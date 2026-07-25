namespace Mes.Library.ShopfloorCommands.Hub;

/// <summary>
/// Interface for controlling shopfloor command hub operations.
/// <para>
/// This interface provides methods for sending and broadcasting commands through
/// the shopfloor command hub from server-side code.
/// </para>
/// </summary>
/// <remarks>
/// Implementations of this interface are used by server-side code (e.g., API controllers)
/// to send commands through the hub without requiring a SignalR client connection.
/// <para>
/// The interface returns <see cref="ShopfloorCommandResponse"/> to indicate success or
/// failure of the operation, allowing callers to handle errors appropriately.
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandHubController"/>
/// <seealso cref="ShopfloorCommandResponse"/>
/// <seealso cref="IShopfloorCommand"/>
public interface IShopfloorCommandHubController
{
    /// <summary>
    /// Asynchronously sends a command to a specific shopfloor through the hub.
    /// <para>
    /// This method routes the command to the shopfloor identified by
    /// <see cref="IShopfloorCommand.ReceiverShopfloorKey"/>. The command will be
    /// delivered via SignalR to the target shopfloor if it is currently connected.
    /// </para>
    /// </summary>
    /// <param name="command">The command to send. Must have a valid <see cref="IShopfloorCommand.ReceiverShopfloorKey"/>.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result indicates whether
    /// the command was successfully sent (<see cref="ShopfloorCommandResponse.Success"/>) or
    /// if an error occurred (<see cref="ShopfloorCommandResponse.Failure"/>).
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the command parameter is null.</exception>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via the cancellation token.</exception>
    Task<ShopfloorCommandResponse> SendAsync(IShopfloorCommand command, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously broadcasts a command to all connected shopfloors through the hub.
    /// <para>
    /// This method sends the specified command to all currently connected shopfloors
    /// via SignalR. It can be used for commands that need to be received by all shopfloors
    /// simultaneously.
    /// </para>
    /// </summary>
    /// <param name="command">The command to broadcast to all connected shopfloors.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result indicates whether
    /// the command was successfully broadcast (<see cref="ShopfloorCommandResponse.Success"/>) or
    /// if an error occurred (<see cref="ShopfloorCommandResponse.Failure"/>).
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the command parameter is null.</exception>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via the cancellation token.</exception>
    Task<ShopfloorCommandResponse> BroadcastAsync(IShopfloorCommand command, CancellationToken cancellationToken);
}