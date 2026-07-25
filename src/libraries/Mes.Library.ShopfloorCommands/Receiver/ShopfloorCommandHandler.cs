using Mes.Library.RabbitMQ.Producer;

namespace Mes.Library.ShopfloorCommands.Receiver;

/// <summary>
/// Default implementation of <see cref="IShopfloorCommandHandler"/> that forwards
/// commands to the RabbitMQ message bus.
/// <para>
/// This handler acts as a fallback for commands that don't have a specific handler
/// registered. It publishes the command to the RabbitMQ message bus for processing.
/// </para>
/// </summary>
/// <remarks>
/// This class is internal and is automatically registered with the DI container when
/// <see cref="ReceiverServiceCollectionExtensions.AddShopfloorCommandReceiver"/> is called.
/// <para>
/// When a command is received and no specific handler (implementing
/// <see cref="IShopfloorCommandHandler{TCommand}"/>) is found for the command type,
/// this handler will publish the command to the RabbitMQ message bus, allowing
/// it to be processed by other services or persistently stored.
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandHandler"/>
/// <seealso cref="IMessagePublisher"/>
/// <seealso cref="ShopfloorCommandReceiver"/>
internal sealed class ShopfloorCommandHandler(IMessagePublisher messagePublisher) : IShopfloorCommandHandler
{
    /// <summary>
    /// Asynchronously handles a shopfloor command by publishing it to the RabbitMQ message bus.
    /// <para>
    /// This method uses the <see cref="IMessagePublisher"/> to publish the command as a
    /// message, enabling it to be processed by consumers on the message bus.
    /// </para>
    /// </summary>
    /// <param name="command">The command to handle and publish.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous publish operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the command parameter is null.</exception>
    public Task HandleAsync(IShopfloorCommand command, CancellationToken cancellationToken)
    {
        return messagePublisher.PublishAsync(command, cancellationToken);
    }
}