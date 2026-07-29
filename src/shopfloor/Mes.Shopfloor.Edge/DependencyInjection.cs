using System.Reflection;
using Mes.Shopfloor.Edge.SharedKernel;
using Mes.Shopfloor.Edge.ShopfloorCommands;
using Mes.Shopfloor.Edge.Synchronization;

namespace Mes.Shopfloor.Edge;

internal static class DependencyInjection
{
    public static IServiceCollection AddMesShopfloorEdge(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = GetAssemblies();

        services.AddMesShopfloorEdgeSharedKernel(configuration, assemblies);
        services.AddMesShopfloorEdgeShopfloorCommands();
        services.AddMesShopfloorEdgeSynchronization();

        return services;
    }

    private static Assembly[] GetAssemblies()
    {
        return YieldAssemblyNames().Select(Assembly.Load).ToArray();
    }

    private static IEnumerable<string> YieldAssemblyNames()
    {
        yield return "Mes.Shopfloor.Edge.SharedKernel";
        yield return "Mes.Shopfloor.Edge.ShopfloorCommands";
        yield return "Mes.Shopfloor.Edge.Synchronization";
    }
}