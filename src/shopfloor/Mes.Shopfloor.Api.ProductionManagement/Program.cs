using System.Reflection;
using DandyEndpoints;
using NLog.Extensions.Logging;
using Scalar.AspNetCore;

namespace Mes.Shopfloor.Api.ProductionManagement;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var assemblies = new List<Assembly> { typeof(Program).Assembly };

        builder.Services.AddOpenApi();
        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        });
        builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();
        builder.Services.AddProductionManagement(builder.Configuration, assemblies);

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference("/docs", options =>
            {
                options.Title = "MES Shopfloor Production Management API Reference";
                options.Agent = new() { Disabled = true };
            });
        }

        app.UseHttpsRedirection();

        app.MapDandyEndpoints();

        app.Run();
    }
}