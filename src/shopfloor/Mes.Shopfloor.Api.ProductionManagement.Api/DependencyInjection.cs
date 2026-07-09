using System.Reflection;
using Mes.Shopfloor.Api.SharedKernel.ProductionManagement.Api.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.ProductionManagement.Api.Subdomains.DataCollection.Repositories;
using Mes.Shopfloor.Api.SharedKernel.ProductionManagement.Api.Subdomains.ProductDefinition.Repositories;
using Mes.Shopfloor.Api.SharedKernel.ProductionManagement.Api.Subdomains.Resources.Repositories;
using Mes.Shopfloor.Api.SharedKernel.ProductionManagement.Api.Subdomains.Scheduling.Repositories;

namespace Mes.Shopfloor.Api.ProductionManagement.Api;

internal static class DependencyInjection
{
    public static IServiceCollection AddProductionManagement(this IServiceCollection services, IConfiguration configuration, List<Assembly> assemblies)
    {
        // Data collection
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IStateRepository, StateRepository>();
        services.AddScoped<IRejectGroupRepository, RejectGroupRepository>();
        services.AddScoped<IStateGroupRepository, StateGroupRepository>();

        // Product definition
        services.AddScoped<IProductionProcessRepository, ProductionProcessRepository>();
        
        // Resources
        services.AddScoped<IProductionUnitRepository, ProductionUnitRepository>();
        services.AddScoped<IWorkerRepository, WorkerRepository>();
        
        // Scheduling
        services.AddScoped<IProductionUnitTaskRepository, ProductionUnitTaskRepository>();
        services.AddScoped<IProductionUnitScheduleRepository, ProductionUnitScheduleRepository>();
        services.AddScoped<IProductionOrderRepository, ProductionOrderRepository>();

        return services;
    }
}