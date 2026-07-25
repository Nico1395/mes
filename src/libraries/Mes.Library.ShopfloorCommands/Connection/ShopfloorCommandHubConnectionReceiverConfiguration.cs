namespace Mes.Library.ShopfloorCommands.Connection;

/// <summary>
/// Configuration class for the shopfloor command hub connection receiver.
/// <para>
/// This class holds the configuration settings required to establish a connection
/// between a shopfloor and the command hub.
/// </para>
/// </summary>
/// <remarks>
/// This configuration is used by the <see cref="ShopfloorCommandHubConnectionFactory"/>
/// to create SignalR connections. It must be populated with the shopfloor's unique
/// key and the base URL of the command hub before connections can be established.
/// <para>
/// Configuration is typically set up using the
/// <see cref="ShopfloorCommandHubConnectionReceiverConfigurationBuilder"/> via the
/// <see cref="CommandHubConnectionServiceCollectionExtensions.AddShopfloorCommandHubConnection"/>
/// extension method.
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandHubConnectionReceiverConfigurationBuilder"/>
/// <seealso cref="CommandHubConnectionServiceCollectionExtensions.AddShopfloorCommandHubConnection"/>
/// <seealso cref="ShopfloorCommandHubConnectionFactory"/>
public sealed class ShopfloorCommandHubConnectionReceiverConfiguration
{
    /// <summary>
    /// Gets or sets the unique identifier key for this shopfloor.
    /// <para>
    /// This key is used to identify the shopfloor when registering with the command hub.
    /// It must be set to a non-null, non-whitespace value before attempting to create a connection.
    /// </para>
    /// </summary>
    /// <value>The unique key identifying this shopfloor.</value>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to create a connection if this property is null or whitespace.
    /// </exception>
    public string? ShopfloorKey { get; set; }

    /// <summary>
    /// Gets or sets the base URL of the shopfloor command hub.
    /// <para>
    /// This URL is used to establish the SignalR connection to the hub. The factory will
    /// append the appropriate path (/cmd/v1/) to this base URL.
    /// It must be set to a non-null, non-whitespace value before attempting to create a connection.
    /// </para>
    /// </summary>
    /// <value>The base URL of the command hub (e.g., "https://example.com").</value>
    /// <exception cref="InvalidOperationException">
    /// Thrown when attempting to create a connection if this property is null or whitespace.
    /// </exception>
    public string? HubBaseUrl { get; set; }
}