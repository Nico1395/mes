using System.Reflection;
using DandyEndpoints;
using DandyMediator;
using DandyMediator.Validation;
using Mes.Shopfloor.Api.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Repositories;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Repositories;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Repositories;
using Mes.Shopfloor.Shared.Messaging.Connections;
using Mes.Shopfloor.Shared.Messaging.Consumer.Configuration;
using Mes.Shopfloor.Shared.Messaging.Producer;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement;

internal static class DependencyInjection
{
    public static IServiceCollection AddProductionManagement(this IServiceCollection services, IConfiguration configuration, List<Assembly> assemblies)
    {
        // Infrastructure
        services.AddObjectMapper();
        services.AddRabbitMQConnection(connection =>
        {
            connection.ConnectToCluster(
                userName: "dev",
                password: "dev",
                nodes: ["localhost:5672", "localhost:5673"]);
        });
        services.AddRabbitMQProducer();
        services.AddRabbitMQConsumer();
        services.AddDandyMediator(cfg =>
        {
            cfg.UseValidation();
            cfg.ScanInAssemblies(assemblies);
        });
        services.AddDandyEndpoints(cfg =>
        {
            cfg.ScanInAssemblies(assemblies);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<DbContext, ProductionManagementDbContext>();

        // Data collection
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IStateRepository, StateRepository>();
        services.AddScoped<IRejectGroupRepository, RejectGroupRepository>();
        services.AddScoped<IStateGroupRepository, StateGroupRepository>();

        // Resources
        services.AddScoped<IProductionUnitRepository, ProductionUnitRepository>();

        // Scheduling
        services.AddScoped<IProductionUnitTaskRepository, ProductionUnitTaskRepository>();
        services.AddScoped<IProductionUnitScheduleRepository, ProductionUnitScheduleRepository>();
        
        return services;
    }
}