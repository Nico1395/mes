using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Edge.Synchronization;

public static class DependencyInjection
{
    public static IServiceCollection AddMesShopfloorEdgeSynchronization(this IServiceCollection services)
    {
        return services;
    }
}