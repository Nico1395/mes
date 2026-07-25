using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Receiver;

/// <summary>
/// Extension methods for configuring shopfloor command receiver services in the DI container.
/// </summary>
/// <remarks>
/// This class provides extension methods for <see cref="IServiceCollection"/> to simplify
/// the registration of shopfloor command receiver-related services.
/// <para>
/// The registered services include:
/// <list type="bullet">
/// <item><description>The configuration for the receiver</description></item>
/// <item><description>The receiver (<see cref="IShopfloorCommandReceiver"/>) implementation</description></item>
/// <item><description>The universal command handler (<see cref="IShopfloorCommandHandler"/>) implementation</description></item>
/// <item><description>All discovered command handlers (implementing <see cref="IShopfloorCommandHandler{TCommand}"/>) from the specified assemblies</description></item>
/// </list>
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandReceiver"/>
/// <seealso cref="IShopfloorCommandHandler"/>
/// <seealso cref="IShopfloorCommandHandler{TCommand}"/>
/// <seealso cref="ShopfloorCommandReceiverConfigurationBuilder"/>
public static class ReceiverServiceCollectionExtensions
{
    /// <summary>
    /// List of generic handler interface types to look for when scanning assemblies.
    /// </summary>
    private static readonly IReadOnlyList<Type> _consumerTypes =
    [
        typeof(IShopfloorCommandHandler<>),
    ];

    /// <summary>
    /// Adds the shopfloor command receiver services to the DI container.
    /// <para>
    /// This method registers all necessary services for receiving and processing
    /// shopfloor commands, including automatic discovery and registration of command handlers.
    /// </para>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">An action to configure the <see cref="ShopfloorCommandReceiverConfigurationBuilder"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> instance for method chaining.</returns>
    /// <remarks>
    /// This method should be called to enable shopfloor command receiving capabilities.
    /// The configuration action should specify the assemblies to scan for command handlers.
    /// <para>
    /// Example usage:
    /// <code>
    /// services.AddShopfloorCommandReceiver(builder => builder
    ///     .ScanInAssemblies(typeof(StartProductionCommandHandler).Assembly, typeof(StopProductionCommandHandler).Assembly));
    /// </code>
    /// </para>
    /// <para>
    /// The method performs the following registrations:
    /// <list type="number">
    /// <item><description>Registers the configuration with the DI container as a singleton</description></item>
    /// <item><description>Registers the <see cref="IShopfloorCommandReceiver"/> implementation as a singleton</description></item>
    /// <item><description>Registers the universal <see cref="IShopfloorCommandHandler"/> implementation as a singleton</description></item>
    /// <item><description>Scans the specified assemblies for command handler implementations and registers each as a transient service</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if the services parameter is null.</exception>
    public static IServiceCollection AddShopfloorCommandReceiver(this IServiceCollection services, Action<ShopfloorCommandReceiverConfigurationBuilder> configuration)
    {
        var builder = new ShopfloorCommandReceiverConfigurationBuilder();
        configuration(builder);
        var cfg = builder.Build();

        services.AddSingleton(cfg);
        services.AddSingleton<IShopfloorCommandReceiver, ShopfloorCommandReceiver>();
        services.AddSingleton<IShopfloorCommandHandler, ShopfloorCommandHandler>();

        AddHandlers(services, cfg.Assemblies);

        return services;
    }

    /// <summary>
    /// Scans the specified assemblies for command handler implementations and registers them with the DI container.
    /// <para>
    /// This method looks for classes that implement <see cref="IShopfloorCommandHandler{TCommand}"/>
    /// for any command type, and registers each matching class as a transient service.
    /// </para>
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add handler services to.</param>
    /// <param name="assemblies">The assemblies to scan for command handler implementations.</param>
    /// <remarks>
    /// This method performs the following for each suitable class found:
    /// <list type="number">
    /// <item><description>Checks if the class implements any generic interface from <see cref="_consumerTypes"/></description></item>
    /// <item><description>For each matching generic interface implementation, registers the class as a transient service for that interface type</description></item>
    /// </list>
    /// This enables the receiver to automatically resolve and invoke the appropriate handler
    /// for each command type.
    /// </remarks>
    private static void AddHandlers(IServiceCollection services, IReadOnlyList<Assembly> assemblies)
    {
        var handlerTypes = assemblies.SelectMany(a => a.DefinedTypes).Where(t => t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false });
        foreach (var implementationType in handlerTypes)
        {
            var interfaces = implementationType.ImplementedInterfaces;
            foreach (var @interface in interfaces)
            {
                if (!@interface.IsGenericType)
                    continue;

                var genericDefinition = @interface.GetGenericTypeDefinition();
                if (_consumerTypes.Contains(genericDefinition))
                    services.AddTransient(@interface, implementationType);
            }
        }
    }
}