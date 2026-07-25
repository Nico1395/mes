using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Receiver;

public static class ReceiverServiceCollectionExtensions
{
    private static readonly IReadOnlyList<Type> _consumerTypes =
    [
        typeof(IShopfloorCommandHandler<>),
    ];

    public static IServiceCollection AddShopfloorCommandReceiver(this IServiceCollection services, Action<ShopfloorCommandReceiverConfigurationBuilder> configuration)
    {
        var builder = new ShopfloorCommandReceiverConfigurationBuilder();
        configuration(builder);
        var cfg = builder.Build();
        
        services.AddSingleton(cfg);
        services.AddSingleton<IShopfloorCommandHubConnectionFactory, ShopfloorCommandHubConnectionFactory>();
        services.AddSingleton<IShopfloorCommandReceiver, ShopfloorCommandReceiver>();
        services.AddSingleton<IShopfloorCommandHandler, ShopfloorCommandHandler>();

        AddHandlers(services, cfg.Assemblies);

        return services;
    }

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