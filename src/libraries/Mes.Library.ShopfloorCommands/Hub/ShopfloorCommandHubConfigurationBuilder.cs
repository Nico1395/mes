namespace Mes.Library.ShopfloorCommands.Hub;

/// <summary>
/// Builder class for constructing <see cref="ShopfloorCommandHubConfiguration"/> instances.
/// <para>
/// This class provides a fluent interface for configuring the shopfloor command hub.
/// </para>
/// </summary>
/// <remarks>
/// This builder is used in the DI configuration to set up the hub settings.
/// It is typically used within the
/// <see cref="CommandHubServiceCollectionExtensions.AddShopfloorCommandHub"/> method call.
/// <para>
/// Example usage:
/// <code>
/// services.AddShopfloorCommandHub(builder => builder
///     .WithRedisUrl("localhost:6379,password=,ssl=False,abortConnect=False"));
/// </code>
/// </para>
/// </remarks>
/// <seealso cref="ShopfloorCommandHubConfiguration"/>
/// <seealso cref="CommandHubServiceCollectionExtensions.AddShopfloorCommandHub"/>
public sealed class ShopfloorCommandHubConfigurationBuilder
{
    private readonly ShopfloorCommandHubConfiguration _configuration = new();

    /// <summary>
    /// Builds the <see cref="ShopfloorCommandHubConfiguration"/> instance with the configured settings.
    /// <para>
    /// This method is internal and is called by the DI configuration extension method.
    /// </para>
    /// </summary>
    /// <returns>A configured <see cref="ShopfloorCommandHubConfiguration"/> instance.</returns>
    internal ShopfloorCommandHubConfiguration Build()
    {
        return _configuration;
    }
}