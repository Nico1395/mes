using System.Reflection;
using Marten;

namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence.Marten;

public static class StoreOptionsExtensions
{
    public static StoreOptions ConfigureWithConfigurationsFromAssemblies(this StoreOptions storeOptions, params Assembly[] assemblies)
    {
        var configurationTypes = GetEventStoreConfigurationTypes(assemblies);
        foreach (var configurationType in configurationTypes)
        {
            if (Activator.CreateInstance(configurationType) is not IEventStoreConfiguration configuration)
                continue;
            
            configuration.Configure(storeOptions);
        }

        return storeOptions;
    }

    private static IEnumerable<Type> GetEventStoreConfigurationTypes(Assembly[] assemblies)
    {
        return assemblies.SelectMany(a => a.GetTypes()).Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(typeof(IEventStoreConfiguration)));
    }
}