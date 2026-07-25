using System.Reflection;
using DandyEndpoints;
using DandyMediator;
using DandyMediator.Validation;
using DandyStrategies;
using Marten;
using Mes.Libraries.ObjectMapping;
using Mes.Libraries.RabbitMQ.Connections;
using Mes.Libraries.RabbitMQ.Consumer.Configuration;
using Mes.Libraries.RabbitMQ.Producer;
using Mes.Shopfloor.Api.SharedKernel.Configurations;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence.EntityFrameworkCore;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence.Marten;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Mes.Shopfloor.Api.SharedKernel;

public static class DependencyInjection
{
    public static IServiceCollection AddMesShopfloorSharedKernel(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
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

        // Marten
        var martenConnectionString = configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("No default connection string configured.");
        services.AddMarten(options =>
        {
            options.DatabaseSchemaName = "event_store";
            options.Connection(martenConnectionString);
            options.ConfigureWithConfigurationsFromAssemblies(assemblies);
        });

        // DandyMediator
        services.AddDandyMediator(cfg =>
        {
            cfg.UseValidation();
            cfg.ScanInAssemblies(assemblies);
        });

        // DandyEndpoints
        services.AddDandyEndpoints(cfg =>
        {
            cfg.ScanInAssemblies(assemblies);
        });

        // DandyStrategies
        services.AddDandyStrategies(cfg => cfg.ScanInAssemblies(assemblies));

        // Core persistence
        services.AddDbContext<DbContext, ShopfloorDbContext>();
        services.AddTransient<IInterceptor, DomainInterfaceSaveChangesInterceptor>();

        // Options
        services.AddOptions<AppOptions>().Bind(configuration.GetSection("App"));            // TODO -> Make sure this is containerizable
        
        return services;
    }
}