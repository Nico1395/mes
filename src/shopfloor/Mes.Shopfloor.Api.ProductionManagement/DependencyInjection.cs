using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Api.ProductionManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddMesShopfloorProductionManagement(this IServiceCollection services)
    {
        return services;
    }
}