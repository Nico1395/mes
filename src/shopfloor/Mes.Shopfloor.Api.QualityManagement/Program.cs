using Mes.Shopfloor.Core.Messaging.Connections;
using Mes.Shopfloor.Core.Messaging.Consumer.Configuration;
using Mes.Shopfloor.Core.Messaging.Producer;
using NLog.Extensions.Logging;

namespace Mes.Shopfloor.Api.QualityManagement;

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
        }

        app.UseHttpsRedirection();

        app.Run();
    }
}