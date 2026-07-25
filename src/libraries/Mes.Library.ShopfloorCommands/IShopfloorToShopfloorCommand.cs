namespace Mes.Library.ShopfloorCommands;

/// <summary>
/// Represents a command that is sent from one shopfloor to another.
/// <para>
/// This interface extends <see cref="IShopfloorCommand"/> by adding information about
/// the sending shopfloor, enabling bidirectional communication tracking.
/// </para>
/// </summary>
/// <remarks>
/// Commands implementing this interface are used when a shopfloor needs to send a command
/// to another specific shopfloor. The sender information allows the receiver to potentially
/// send responses or acknowledgments back to the originating shopfloor.
/// </remarks>
/// <seealso cref="IShopfloorCommand"/>
/// <seealso cref="ShopfloorToShopfloorCommand"/>
public interface IShopfloorToShopfloorCommand : IShopfloorCommand
{
    /// <summary>
    /// Gets the unique identifier key of the shopfloor that sent this command.
    /// <para>
    /// This key identifies the originating shopfloor and can be used for response routing.
    /// </para>
    /// </summary>
    /// <value>The sender shopfloor's unique key.</value>
    string SenderShopfloorKey { get; }
}