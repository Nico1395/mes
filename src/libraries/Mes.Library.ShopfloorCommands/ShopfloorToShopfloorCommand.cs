namespace Mes.Library.ShopfloorCommands;

/// <summary>
/// Abstract base class for commands that are sent from one shopfloor to another.
/// <para>
/// This class extends <see cref="ShopfloorCommand"/> and implements
/// <see cref="IShopfloorToShopfloorCommand"/>, providing a concrete base for
/// bidirectional shopfloor command implementations.
/// </para>
/// </summary>
/// <remarks>
/// Inherit from this class to create concrete command types that need to track both
/// the sending and receiving shopfloors. This enables bidirectional communication
/// and response routing.
/// <para>
/// Example usage:
/// <code>
/// public class ProductionStatusRequest : ShopfloorToShopfloorCommand
/// {
///     public required string RequestId { get; init; }
///     public required string[] MachineIds { get; init; }
/// }
/// </code>
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommand"/>
/// <seealso cref="IShopfloorToShopfloorCommand"/>
/// <seealso cref="IShopfloorCommand"/>
public abstract class ShopfloorToShopfloorCommand : ShopfloorCommand, IShopfloorToShopfloorCommand
{
    /// <summary>
    /// Gets the unique identifier key of the shopfloor that sent this command.
    /// <para>
    /// This key identifies the originating shopfloor and can be used for response routing
    /// or to establish bidirectional communication channels.
    /// Must be set before sending the command.
    /// </para>
    /// </summary>
    /// <value>The sender shopfloor's unique key.</value>
    /// <exception cref="InvalidOperationException">Thrown if this property is not set before use.</exception>
    public required string SenderShopfloorKey { get; init; }
}