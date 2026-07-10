using Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.ProductDefinition.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.Resources.Repositories;
using Mes.Shopfloor.Client.ProductionManagement.Scheduling.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.ProductionManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddTerminalProductionManagement(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductionUnitModelRepository, ProductionUnitModelRepository>();
        services.AddScoped<IProductionUnitScheduleModelRepository, ProductionUnitScheduleModelRepository>();
        services.AddScoped<IStateGroupModelRepository, StateGroupModelRepository>();
        services.AddScoped<IRejectGroupModelRepository, RejectGroupModelRepository>();
        services.AddScoped<IProductionOrderModelRepository, ProductionOrderModelRepository>();
        services.AddScoped<IProductionProcessModelRepository, ProductionProcessModelRepository>();
        services.AddScoped<IWorkerModelRepository, WorkerModelRepository>();

        return services;
    }
}