using DandyEndpoints;
using Mes.Shopfloor.Shared.Messaging.Connections;
using Mes.Shopfloor.Shared.Messaging.Consumer.Configuration;
using Mes.Shopfloor.Shared.Messaging.Producer;
using NLog.Extensions.Logging;
using Scalar.AspNetCore;

namespace Mes.Shopfloor.Api.InventoryManagement;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        });
        builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

        builder.Services.AddRabbitMQConnection(connection =>
        {
            connection.ConnectToCluster(
                userName: "dev",
                password: "dev",
                nodes: ["localhost:5672", "localhost:5673"]);
        });
        builder.Services.AddRabbitMQProducer();
        builder.Services.AddRabbitMQConsumer();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference("/docs", options =>
            {
                options.Title = "MES Shopfloor Inventory Management API Reference";
                options.Agent = new() { Disabled = true };
            });
        }

        app.UseHttpsRedirection();

        app.MapDandyEndpoints();

        app.Run();
    }
}