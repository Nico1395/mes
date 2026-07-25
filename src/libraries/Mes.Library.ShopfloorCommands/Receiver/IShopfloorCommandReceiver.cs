namespace Mes.Library.ShopfloorCommands.Receiver;

/// <summary>
/// Interface for receiving shopfloor commands.
/// <para>
/// This interface provides the ability to start receiving commands from the shopfloor
/// command hub. Implementations manage the SignalR connection and command routing.
/// </para>
/// </summary>
/// <remarks>
/// This interface is typically used by shopfloor applications to start listening for
/// incoming commands. The receiver will establish a SignalR connection to the command
/// hub and register handlers for incoming commands.
/// <para>
/// Command routing is based on the command type, with specific handlers (implementing
/// <see cref="IShopfloorCommandHandler{TCommand}"/>) being invoked for known command types,
/// and a fallback handler (implementing <see cref="IShopfloorCommandHandler"/>) for unknown types.
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandReceiver"/>
/// <seealso cref="IShopfloorCommandHandler"/>
/// <seealso cref="IShopfloorCommandHandler{TCommand}"/>
public interface IShopfloorCommandReceiver
{
    /// <summary>
    /// Asynchronously starts receiving commands from the shopfloor command hub.
    /// <para>
    /// This method establishes the SignalR connection to the command hub and sets up
    /// the message handler for incoming commands. After this method returns, the
    /// receiver will be actively listening for and processing commands.
    /// </para>
    /// </summary>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the receive operation.
    /// When canceled, the receiver will stop listening for new commands.
    /// </param>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if required services are not configured or if a connection cannot be established.
    /// </exception>
    /// <exception cref="OperationCanceledException">Thrown if the operation is canceled via the cancellation token.</exception>
    Task StartReceivingAsync(CancellationToken cancellationToken);
}