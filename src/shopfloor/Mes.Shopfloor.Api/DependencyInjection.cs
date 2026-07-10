using System.Reflection;
using Mes.Shopfloor.Api.ProductionManagement;
using Mes.Shopfloor.Api.SharedKernel;

namespace Mes.Shopfloor.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddMesShopfloorApi(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = GetAssemblies();

        services.AddMesShopfloorSharedKernel(configuration, assemblies);
        services.AddMesShopfloorProductionManagement();

        return services;
    }

    private static Assembly[] GetAssemblies()
    {
        return YieldAssemblyNames().Select(Assembly.Load).ToArray();
    }

    private static IEnumerable<string> YieldAssemblyNames()
    {
        yield return "Mes.Shopfloor.Api.SharedKernel";
        yield return "Mes.Shopfloor.Api.ProductionManagement";
    }
}