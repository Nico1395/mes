namespace Mes.Library.ShopfloorCommands.Sender;

/// <summary>
/// Interface for sending shopfloor-to-shopfloor commands.
/// <para>
/// This interface provides the ability to send commands from one shopfloor to another
/// through the command hub and RabbitMQ message bus.
/// </para>
/// </summary>
/// <remarks>
/// Implementations of this interface manage the sending of commands to the command hub,
/// which then forwards them to the appropriate shopfloor or to the RabbitMQ message bus.
/// <para>
/// Commands sent through this interface must implement <see cref="IShopfloorToShopfloorCommand"/>
/// to include both sender and receiver information.
/// </para>
/// <para>
/// The method returns a <see cref="ShopfloorCommandResponse"/> indicating whether the
/// command was successfully sent or if an error occurred.
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandSender"/>
/// <seealso cref="IShopfloorToShopfloorCommand"/>
/// <seealso cref="ShopfloorCommandResponse"/>
public interface IShopfloorCommandSender
{
    /// <summary>
    /// Asynchronously sends a shopfloor-to-shopfloor command.
    /// <para>
    /// This method sends the command through the command hub, which will forward it
    /// to the target shopfloor via SignalR if connected, or to the RabbitMQ message bus
    /// for persistent delivery.
    /// </para>
    /// </summary>
    /// <param name="command">
    /// The command to send. Must implement <see cref="IShopfloorToShopfloorCommand"/>
    /// and have both <see cref="IShopfloorCommand.ReceiverShopfloorKey"/> and
    /// <see cref="IShopfloorToShopfloorCommand.SenderShopfloorKey"/> set.
    /// </param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result indicates whether
    /// the command was successfully sent (<see cref="ShopfloorCommandResponse.Success"/>) or
    /// if an error occurred (<see cref="ShopfloorCommandResponse.Failure"/>).
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the command parameter is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if required properties on the command are not set.
    /// </exception>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via the cancellation token.</exception>
    Task<ShopfloorCommandResponse> SendAsync(IShopfloorToShopfloorCommand command, CancellationToken cancellationToken);
}