using System.Reflection;
using DandyEndpoints;
using DandyMediator;
using DandyMediator.Validation;
using DandyStrategies;
using Marten;
using Mes.Library.Marten;
using Mes.Library.ObjectMapping;
using Mes.Library.RabbitMQ.Connections;
using Mes.Library.RabbitMQ.Consumer.Configuration;
using Mes.Library.RabbitMQ.Producer;
using Mes.Library.Serialization.Json;
using Mes.Shopfloor.Api.SharedKernel.Configurations;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence.EntityFrameworkCore;
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
        services.AddDandyEndpoints(cfg => { cfg.ScanInAssemblies(assemblies); });

        // DandyStrategies
        services.AddDandyStrategies(cfg => cfg.ScanInAssemblies(assemblies));

        // Core persistence
        services.AddDbContext<DbContext, ShopfloorDbContext>();
        services.AddTransient<IInterceptor, DomainInterfaceSaveChangesInterceptor>();

        // Options
        services.AddOptions<AppOptions>().Bind(configuration.GetSection("App"));

        // Serialization
        services.AddMinimalApiJsonOptions();

        return services;
    }
}