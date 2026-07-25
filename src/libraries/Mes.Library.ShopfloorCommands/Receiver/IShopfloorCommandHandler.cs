namespace Mes.Library.ShopfloorCommands.Receiver;

/// <summary>
/// Interface for handling shopfloor commands of any type.
/// <para>
/// This interface provides a generic way to handle commands without knowing their specific type.
/// Implementations can handle any <see cref="IShopfloorCommand"/>.
/// </para>
/// </summary>
/// <remarks>
/// This interface is used as a fallback handler when no specific handler for a command
/// type is registered. It provides a universal way to process commands.
/// <para>
/// Typically, this is implemented by the <see cref="ShopfloorCommandHandler"/>, which
/// forwards commands to the RabbitMQ message bus for processing.
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandHandler{TCommand}"/>
/// <seealso cref="ShopfloorCommandHandler"/>
public interface IShopfloorCommandHandler
{
    /// <summary>
    /// Asynchronously handles a shopfloor command.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous handle operation.</returns>
    Task HandleAsync(IShopfloorCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Generic interface for handling shopfloor commands of a specific type.
/// <para>
/// This interface provides a type-safe way to handle commands of a specific type TCommand.
/// Implementations are automatically discovered and registered based on the generic
/// type parameter.
/// </para>
/// </summary>
/// <typeparam name="TCommand">The specific type of command to handle. Must be a class implementing <see cref="IShopfloorCommand"/>.</typeparam>
/// <remarks>
/// This interface enables the receiver to automatically route commands to the appropriate
/// handler based on the command type. Handlers implementing this interface for a specific
/// command type will be automatically discovered when
/// <see cref="ReceiverServiceCollectionExtensions.AddShopfloorCommandReceiver"/> is called
/// with the appropriate assembly scanning configuration.
/// <para>
/// Example handler implementation:
/// <code>
/// public class StartProductionCommandHandler : IShopfloorCommandHandler&lt;StartProductionCommand&gt;
/// {
///     private readonly IProductionService _productionService;
///     
///     public StartProductionCommandHandler(IProductionService productionService)
///     {
///         _productionService = productionService;
///     }
///     
///     public Task HandleAsync(StartProductionCommand command, CancellationToken cancellationToken)
///     {
///         return _productionService.StartAsync(command.OrderId, command.Quantity, cancellationToken);
///     }
/// }
/// </code>
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandHandler"/>
/// <seealso cref="ReceiverServiceCollectionExtensions.AddShopfloorCommandReceiver"/>
public interface IShopfloorCommandHandler<in TCommand>
    where TCommand : class, IShopfloorCommand
{
    /// <summary>
    /// Asynchronously handles a shopfloor command of type TCommand.
    /// </summary>
    /// <param name="command">The command to handle.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous handle operation.</returns>
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}