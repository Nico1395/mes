using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.Infrastructure.Routine;

public static class RoutineServiceCollectionExtensions
{
    private static readonly IReadOnlyList<Type> _jobTypes =
    [
        typeof(IRoutineJob),
    ];

    public static IServiceCollection AddRoutine(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddSingleton<IRoutine, Routine>();
        services.AddSingleton<IRoutineContext, RoutineContext>();

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