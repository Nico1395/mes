using System.Reflection;
using DandyEndpoints;
using DandyMediator;
using DandyMediator.Validation;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Connections;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer.Configuration;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Producer;
using Mes.Shopfloor.Shared.SharedKernel.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Mes.Shopfloor.Api.SharedKernel;

public static class DependencyInjection
{
    public static IServiceCollection AddMesShopfloorSharedKernel(this IServiceCollection services, Assembly[] assemblies)
    {
        // Logging
        services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        });
        services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

        // Object mapping
        services.AddObjectMapper();

        // RabbitMQ
        services.AddRabbitMQConnection(connection =>
        {
            connection.ConnectToCluster(
                userName: "dev",
                password: "dev",
                nodes: ["localhost:5672", "localhost:5673"]);
        });
        services.AddRabbitMQProducer();
        services.AddRabbitMQConsumer();

        // Mediator
        services.AddDandyMediator(cfg =>
        {
            cfg.UseValidation();
            cfg.ScanInAssemblies(assemblies);
        });

        // Endpoints
        services.AddDandyEndpoints(cfg =>
        {
            cfg.ScanInAssemblies(assemblies);
        });

        // Core persistence
        services.AddDbContext<DbContext, ShopfloorDbContext>();
        services.AddTransient<IInterceptor, DomainInterfaceSaveChangesInterceptor>();

        return services;
    }
}