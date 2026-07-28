using DandyEndpoints;
using Scalar.AspNetCore;

namespace Mes.Hub.Api;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddMesHubApi(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference("/api/reference", options =>
            {
                options.Title = "MES Hub API Reference";
                options.Agent = new() { Disabled = true };
            });
        }

        app.UseHttpsRedirection();

        app.MapDandyEndpoints();
        app.MapGet("/", () => Results.Redirect("/api/reference"));

        app.Run();
    }
}