using Mes.Library.RabbitMQ;

namespace Mes.Library.ShopfloorCommands;

/// <summary>
/// Represents a command that can be sent to a specific shopfloor.
/// <para>
/// This interface extends <see cref="IMessage"/> from the RabbitMQ library, adding
/// the receiver shopfloor key to identify the target shopfloor for the command.
/// </para>
/// </summary>
/// <remarks>
/// Commands implementing this interface are used for shopfloor-to-shopfloor communication
/// and are routed based on the <see cref="ReceiverShopfloorKey"/> property.
/// </remarks>
/// <seealso cref="IMessage"/>
/// <seealso cref="IShopfloorToShopfloorCommand"/>
/// <seealso cref="ShopfloorCommand"/>
public interface IShopfloorCommand : IMessage
{
    /// <summary>
    /// Gets the unique identifier key of the shopfloor that should receive this command.
    /// <para>
    /// This key is used by the command hub to route the command to the correct shopfloor.
    /// </para>
    /// </summary>
    /// <value>The receiver shopfloor's unique key.</value>
    string ReceiverShopfloorKey { get; }
}