using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Connection;

/// <summary>
/// Extension methods for configuring shopfloor command hub connection services in the DI container.
/// </summary>
/// <remarks>
/// This class provides extension methods for <see cref="IServiceCollection"/> to simplify
/// the registration of shopfloor command hub connection-related services.
/// <para>
/// The registered services include:
/// <list type="bullet">
/// <item><description>The configuration for the hub connection</description></item>
/// <item><description>The connection factory (<see cref="IShopfloorCommandHubConnectionFactory"/>) implementation</description></item>
/// <item><description>The connection provider (<see cref="IShopfloorCommandHubConnectionProvider"/>) implementation</description></item>
/// </list>
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandHubConnectionFactory"/>
/// <seealso cref="IShopfloorCommandHubConnectionProvider"/>
/// <seealso cref="ShopfloorCommandHubConnectionReceiverConfigurationBuilder"/>
public static class CommandHubConnectionServiceCollectionExtensions
{
    /// <summary>
    /// Adds the shopfloor command hub connection services to the DI container.
    /// <para>
    /// This method registers all necessary services for establishing and managing
    /// SignalR connections to the shopfloor command hub.
    /// </para>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">An action to configure the <see cref="ShopfloorCommandHubConnectionReceiverConfigurationBuilder"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> instance for method chaining.</returns>
    /// <remarks>
    /// This method must be called to enable shopfloor command sending and receiving capabilities.
    /// The configuration action should set the shopfloor key and hub base URL at a minimum.
    /// <para>
    /// Example usage:
    /// <code>
    /// services.AddShopfloorCommandHubConnection(builder => builder
    ///     .WithShopfloorKey("Shopfloor1")
    ///     .WithHubBaseUrl("https://hub.example.com"));
    /// </code>
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if the services parameter is null.</exception>
    public static IServiceCollection AddShopfloorCommandHubConnection(this IServiceCollection services, Action<ShopfloorCommandHubConnectionReceiverConfigurationBuilder> configuration)
    {
        var builder = new ShopfloorCommandHubConnectionReceiverConfigurationBuilder();
        configuration(builder);
        var cfg = builder.Build();

        services.AddSingleton(cfg);
        services.AddSingleton<IShopfloorCommandHubConnectionFactory, ShopfloorCommandHubConnectionFactory>();
        services.AddSingleton<IShopfloorCommandHubConnectionProvider, ShopfloorCommandHubConnectionHubConnectionProvider>();

        return services;
    }
}