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
using Mes.Library.ShopfloorCommands.Hub;
using Mes.Library.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Mes.Hub.Api.SharedKernel;

public static class DependencyInjection
{
    public static IServiceCollection AddMesHubEdgeSharedKernel(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
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

        // Core persistence - Note: No interceptor, since we will only ever read master data and don't want to temper with timestamps
        // services.AddDbContext<DbContext, HubEdgeDbContext>();

        // SignalR
        services.AddSignalRWithBackplane(configuration["Redis:Url"], "mes:hub:edge");

        // Shopfloor commands
        services.AddShopfloorCommandHub();

        // Serialization
        services.AddMinimalApiJsonOptions();

        return services;
    }
}