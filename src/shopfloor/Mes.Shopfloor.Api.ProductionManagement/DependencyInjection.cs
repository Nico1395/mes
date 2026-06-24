using System.Reflection;
using DandyEndpoints;
using DandyMediator;
using DandyMediator.Validation;
using Mes.Shopfloor.Api.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis.Repositories;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Repositories;
using Mes.Shopfloor.Core.Messaging.Connections;
using Mes.Shopfloor.Core.Messaging.Consumer.Configuration;
using Mes.Shopfloor.Core.Messaging.Producer;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement;

internal static class DependencyInjection
{
    public static IServiceCollection AddProductionManagement(this IServiceCollection services, IConfiguration configuration, List<Assembly> assemblies)
    {
        // Infrastructure
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

        // Analysis
        services.AddScoped<IProductionUnitStatusRepository, ProductionUnitStatusRepository>();
        services.AddScoped<IProductionUnitStateRepository, ProductionUnitStateRepository>();

        // Resources
        services.AddScoped<IProductionUnitRepository, ProductionUnitRepository>();

        return services;
    }
}