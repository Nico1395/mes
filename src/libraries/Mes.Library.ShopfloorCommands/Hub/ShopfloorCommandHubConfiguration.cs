namespace Mes.Library.ShopfloorCommands.Hub;

/// <summary>
/// Configuration class for the shopfloor command hub.
/// <para>
/// This class holds the configuration settings required for the shopfloor command hub,
/// particularly the Redis connection settings for SignalR backplane support.
/// </para>
/// </summary>
/// <remarks>
/// This configuration is used when setting up the SignalR infrastructure for the
/// shopfloor command hub. The Redis URL is required for enabling the Redis backplane,
/// which allows SignalR to work in scaled-out scenarios with multiple servers.
/// <para>
/// Configuration is typically set up using the
/// <see cref="ShopfloorCommandHubConfigurationBuilder"/> via the
/// <see cref="CommandHubServiceCollectionExtensions.AddShopfloorCommandHub"/> extension method.
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandHubConfigurationBuilder"/>
/// <seealso cref="CommandHubServiceCollectionExtensions.AddShopfloorCommandHub"/>
public sealed class ShopfloorCommandHubConfiguration
{
    /// <summary>
    /// Gets or sets the Redis connection URL for the SignalR backplane.
    /// <para>
    /// This URL is used to configure the Redis backplane for SignalR, enabling
    /// real-time communication across multiple servers in a scaled-out environment.
    /// The format is typically "&lt;host&gt;:&lt;port&gt;,password=&lt;password&gt;,ssl=False,abortConnect=False".
    /// </para>
    /// </summary>
    /// <value>The Redis connection URL.</value>
    /// <exception cref="InvalidAsynchronousStateException">
    /// Thrown if this property is null when attempting to configure SignalR.
    /// </exception>
    public string? RedisUrl { get; set; }
}