using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Sender;

/// <summary>
/// Extension methods for configuring shopfloor command sender services in the DI container.
/// </summary>
/// <remarks>
/// This class provides extension methods for <see cref="IServiceCollection"/> to simplify
/// the registration of shopfloor command sender-related services.
/// <para>
/// The registered services include:
/// <list type="bullet">
/// <item><description>The sender (<see cref="IShopfloorCommandSender"/>) implementation</description></item>
/// </list>
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandSender"/>
/// <seealso cref="ShopfloorCommandSender"/>
public static class SenderServiceCollectionExtensions
{
    /// <summary>
    /// Adds the shopfloor command sender service to the DI container.
    /// <para>
    /// This method registers the <see cref="IShopfloorCommandSender"/> implementation,
    /// enabling shopfloor-to-shopfloor command sending capabilities.
    /// </para>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> instance for method chaining.</returns>
    /// <remarks>
    /// This method should be called to enable shopfloor command sending capabilities.
    /// <para>
    /// Note: This method assumes that the connection provider services have been registered
    /// separately, typically via <see cref="Connection.CommandHubConnectionServiceCollectionExtensions.AddShopfloorCommandHubConnection"/>
    /// or as part of the receiver setup.
    /// </para>
    /// <para>
    /// Example usage:
    /// <code>
    /// services.AddShopfloorCommandSender();
    /// </code>
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if the services parameter is null.</exception>
    public static IServiceCollection AddShopfloorCommandSender(this IServiceCollection services)
    {
        services.AddSingleton<IShopfloorCommandSender, ShopfloorCommandSender>();

        return services;
    }
}