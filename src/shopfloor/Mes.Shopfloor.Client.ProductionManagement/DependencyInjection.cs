using Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.Resources.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.Scheduling.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.ProductionManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddTerminalProductionManagement(this IServiceCollection services, IConfiguration configuration)
    {
        var productionManagementUrl = configuration["Apis:ProductionManagementUrl"] ?? throw new InvalidOperationException("No URL of the production management API configured.");
        services.AddHttpClient("pm", cfg =>
        {
            cfg.BaseAddress = new Uri(productionManagementUrl);
        });

        services.AddSingleton<IProductionUnitModelRepository, ProductionUnitModelRepository>();
        services.AddSingleton<IProductionUnitScheduleModelRepository, ProductionUnitScheduleModelRepository>();
        services.AddSingleton<IStateGroupModelRepository, StateGroupModelRepository>();
        services.AddSingleton<IRejectGroupModelRepository, RejectGroupModelRepository>();

        return services;
    }
}