using Mes.Shopfloor.Core.Messaging.Consumer.Configuration;

namespace Mes.Shopfloor.Api.Orders;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddConsumerConnection(connection =>
        {
            connection.ConfigureFactory(factory =>
            {
                factory.UserName = "dev";
                factory.Password = "dev";
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