using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Hub;

/// <summary>
/// Extension methods for configuring shopfloor command hub services in the DI container.
/// </summary>
/// <remarks>
/// This class provides extension methods for <see cref="IServiceCollection"/> to simplify
/// the registration of shopfloor command hub-related services.
/// <para>
/// The registered services include:
/// <list type="bullet">
/// <item><description>The configuration for the hub</description></item>
/// <item><description>SignalR with StackExchange Redis backplane support</description></item>
/// <item><description>The hub controller (<see cref="IShopfloorCommandHubController"/>) implementation</description></item>
/// </list>
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandHubController"/>
/// <seealso cref="ShopfloorCommandHub"/>
/// <seealso cref="ShopfloorCommandHubConfigurationBuilder"/>
public static class CommandHubServiceCollectionExtensions
{
    /// <summary>
    /// Adds the shopfloor command hub services to the DI container.
    /// <para>
    /// This method registers all necessary services for hosting the shopfloor command hub,
    /// including SignalR with Redis backplane support for scaled-out scenarios.
    /// </para>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">An action to configure the <see cref="ShopfloorCommandHubConfigurationBuilder"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> instance for method chaining.</returns>
    /// <remarks>
    /// This method must be called on the server application to enable the shopfloor command hub.
    /// The configuration action should set the Redis URL for the SignalR backplane.
    /// <para>
    /// After calling this method, you should also call
    /// <see cref="WebApplicationExtensions.MapShopfloorCommandHub"/> to map the hub route.
    /// </para>
    /// <para>
    /// Example usage:
    /// <code>
    /// services.AddShopfloorCommandHub(builder => builder
    ///     .WithRedisUrl("localhost:6379,password=,ssl=False,abortConnect=False"));
    /// </code>
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if the services parameter is null.</exception>
    /// <exception cref="InvalidAsynchronousStateException">Thrown if the Redis URL is not configured.</exception>
    public static IServiceCollection AddShopfloorCommandHub(this IServiceCollection services, Action<ShopfloorCommandHubConfigurationBuilder> configuration)
    {
        var builder = new ShopfloorCommandHubConfigurationBuilder();
        configuration(builder);
        var cfg = builder.Build();

        services.AddSingleton(cfg);
        services.AddTransient<IShopfloorCommandHubController, ShopfloorCommandHubController>();

        return services;
    }
}