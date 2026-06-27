using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;

public static class TerminalRoutineServiceCollectionExtensions
{
    private static readonly IReadOnlyList<Type> _jobTypes =
    [
        typeof(ITerminalRoutineJob),
    ];

    public static IServiceCollection AddTerminalRoutine(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddScoped<ITerminalRoutine, TerminalRoutine>();
        services.AddSingleton<ITerminalRoutineContext, TerminalRoutineContext>(); // Maybe this is not the best idea

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
                    services.AddScoped(@interface, implementationType);
            }
        }
    }
}