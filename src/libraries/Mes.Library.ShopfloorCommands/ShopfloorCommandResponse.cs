namespace Mes.Library.ShopfloorCommands;

/// <summary>
/// Represents the possible responses when sending a shopfloor command.
/// <para>
/// This enumeration is used to indicate the success or failure status of command operations.
/// </para>
/// </summary>
/// <remarks>
/// This enum is returned by the <see cref="IShopfloorCommandSender.SendAsync"/> and
/// <see cref="IShopfloorCommandHubController"/> methods to indicate whether the command
/// was successfully processed or if an error occurred.
/// </remarks>
/// <seealso cref="IShopfloorCommandSender.SendAsync"/>
/// <seealso cref="IShopfloorCommandHubController.SendAsync"/>
/// <seealso cref="IShopfloorCommandHubController.BroadcastAsync"/>
public enum ShopfloorCommandResponse
{
    /// <summary>
    /// Indicates that the command sending or processing operation failed.
    /// <para>
    /// This can occur due to connection issues, routing failures, or exceptions
    /// during command processing.
    /// </para>
    /// </summary>
    Failure = 0,

    /// <summary>
    /// Indicates that the command sending or processing operation succeeded.
    /// <para>
    /// The command was successfully delivered to the intended recipient(s).
    /// </para>
    /// </summary>
    Success = 1,
}