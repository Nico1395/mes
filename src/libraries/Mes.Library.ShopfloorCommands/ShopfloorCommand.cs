using Mes.Library.RabbitMQ;

namespace Mes.Library.ShopfloorCommands;

/// <summary>
/// Abstract base class for shopfloor commands that can be sent to a specific shopfloor.
/// <para>
/// This class extends <see cref="Message"/> from the RabbitMQ library and implements
/// <see cref="IShopfloorCommand"/>, providing a concrete base for command implementations.
/// </para>
/// </summary>
/// <remarks>
/// Inherit from this class to create concrete command types. The <see cref="ReceiverShopfloorKey"/>
/// property must be set to identify the target shopfloor.
/// <para>
/// Example usage:
/// <code>
/// public class StartProductionCommand : ShopfloorCommand
/// {
///     public required string OrderId { get; init; }
///     public required int Quantity { get; init; }
/// }
/// </code>
/// </para>
/// </remarks>
/// <seealso cref="Message"/>
/// <seealso cref="IShopfloorCommand"/>
/// <seealso cref="ShopfloorToShopfloorCommand"/>
public abstract class ShopfloorCommand : Message, IShopfloorCommand
{
    /// <summary>
    /// Gets the unique identifier key of the shopfloor that should receive this command.
    /// <para>
    /// This key is used by the command hub to route the command to the correct shopfloor.
    /// Must be set before sending the command.
    /// </para>
    /// </summary>
    /// <value>The receiver shopfloor's unique key.</value>
    /// <exception cref="InvalidOperationException">Thrown if this property is not set before use.</exception>
    public required string ReceiverShopfloorKey { get; init; }
}