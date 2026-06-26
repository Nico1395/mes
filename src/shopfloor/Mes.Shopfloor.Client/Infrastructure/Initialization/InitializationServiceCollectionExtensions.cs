using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.Infrastructure.Initialization;

public static class InitializationServiceCollectionExtensions
{
    private static readonly IReadOnlyList<Type> _jobTypes =
    [
        typeof(IInitializationJob),
    ];

    public static IServiceCollection AddInitialization(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddSingleton<IInitializer, Initializer>();
        AddJobs(services, assemblies);

        return services;
    }

    private static void AddJobs(IServiceCollection services, Assembly[] assemblies)
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
                if (_jobTypes.Contains(genericDefinition))
                    services.AddSingleton(@interface, implementationType);
            }
        }
    }
}