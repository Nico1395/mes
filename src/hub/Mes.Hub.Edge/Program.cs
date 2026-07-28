using Scalar.AspNetCore;

namespace Mes.Hub.Edge;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddMesHubEdge(builder.Configuration);

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference("/api/reference", options =>
            {
                options.Title = "MES Hub Edge Reference";
                options.Agent = new() { Disabled = true };
            });
        }

        app.UseHttpsRedirection();

        app.MapHubEdge();

        app.Run();
    }
}