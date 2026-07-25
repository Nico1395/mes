namespace Mes.Library.ShopfloorCommands.Connection;

/// <summary>
/// Builder class for constructing <see cref="ShopfloorCommandHubConnectionReceiverConfiguration"/> instances.
/// <para>
/// This class provides a fluent interface for configuring the shopfloor command hub connection.
/// </para>
/// </summary>
/// <remarks>
/// This builder is used in the DI configuration to set up the connection settings.
/// It is typically used within the
/// <see cref="CommandHubConnectionServiceCollectionExtensions.AddShopfloorCommandHubConnection"/>
/// method call.
/// <para>
/// Example usage:
/// <code>
/// services.AddShopfloorCommandHubConnection(builder => builder
///     .WithShopfloorKey("Shopfloor1")
///     .WithHubBaseUrl("https://hub.example.com"));
/// </code>
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandHubConnectionReceiverConfiguration"/>
/// <seealso cref="CommandHubConnectionServiceCollectionExtensions.AddShopfloorCommandHubConnection"/>
public sealed class ShopfloorCommandHubConnectionReceiverConfigurationBuilder
{
    private readonly ShopfloorCommandHubConnectionReceiverConfiguration _configuration = new();

    /// <summary>
    /// Sets the unique identifier key for the shopfloor.
    /// <para>
    /// This key will be used to identify the shopfloor when registering with the command hub.
    /// </para>
    /// </summary>
    /// <param name="shopfloorKey">The unique key identifying this shopfloor.</param>
    /// <returns>The builder instance for method chaining.</returns>
    public ShopfloorCommandHubConnectionReceiverConfigurationBuilder WithShopfloorKey(string shopfloorKey)
    {
        _configuration.ShopfloorKey = shopfloorKey;
        return this;
    }

    /// <summary>
    /// Sets the base URL of the shopfloor command hub.
    /// <para>
    /// This URL will be used to establish the SignalR connection to the hub.
    /// </para>
    /// </summary>
    /// <param name="baseUrl">The base URL of the command hub (e.g., "https://example.com").</param>
    /// <returns>The builder instance for method chaining.</returns>
    public ShopfloorCommandHubConnectionReceiverConfigurationBuilder WithHubBaseUrl(string baseUrl)
    {
        _configuration.HubBaseUrl = baseUrl;
        return this;
    }

    /// <summary>
    /// Builds the <see cref="ShopfloorCommandHubConnectionReceiverConfiguration"/> instance with the configured settings.
    /// <para>
    /// This method is internal and is called by the DI configuration extension method.
    /// </para>
    /// </summary>
    /// <returns>A configured <see cref="ShopfloorCommandHubConnectionReceiverConfiguration"/> instance.</returns>
    internal ShopfloorCommandHubConnectionReceiverConfiguration Build()
    {
        return _configuration;
    }
}