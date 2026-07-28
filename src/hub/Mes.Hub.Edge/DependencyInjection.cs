using System.Reflection;
using Mes.Hub.Edge.SharedKernel;
using Mes.Hub.Edge.Synchronization;

namespace Mes.Hub.Edge;

internal static class DependencyInjection
{
    public static IServiceCollection AddMesHubEdge(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = GetAssemblies();

        services.AddMesHubEdgeSharedKernel(configuration, assemblies);
        services.AddMesHubEdgeSynchronization();

        return services;
    }

    private static Assembly[] GetAssemblies()
    {
        return YieldAssemblyNames().Select(Assembly.Load).ToArray();
    }

    private static IEnumerable<string> YieldAssemblyNames()
    {
        yield return "Mes.Hub.Api.SharedKernel";
        yield return "Mes.Hub.Api.Synchronization";
    }
}