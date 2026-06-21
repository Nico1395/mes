using Mes.Shopfloor.Core.Messaging.Connections;
using Mes.Shopfloor.Core.Messaging.Consumer.Configuration;
using Mes.Shopfloor.Core.Messaging.Producer;
using NLog.Extensions.Logging;

namespace Mes.Shopfloor.Api.Orders;

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

        builder.Services.AddRabbitMQConnection();
        builder.Services.AddRabbitMQProducer();
        builder.Services.AddRabbitMQConsumer(connection =>
        {
            connection.ConfigureFactory(factory =>
            {
                factory.UserName = "dev";
                factory.Password = "dev";
                factory.UseClustering("localhost:5672", "localhost:5673");
            });
            connection.AddListeningChannel("terminal", "api.order", channel =>
            {
                channel.WithRoutingKey("quantity.produced");
            });
            connection.AddListeningChannel("terminal", "api.status", channel =>
            {
                channel.WithRoutingKey("quantity.produced");
                channel.WithRoutingKey("state.changed");
                channel.WithRoutingKey("shift.completed");
                channel.WithRoutingKey("shift.started");
                channel.WithRoutingKey("shift.break.started");
            });
        });

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.Run();
    }
}