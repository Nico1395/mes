using DandyEndpoints;
using Scalar.AspNetCore;

namespace Mes.Shopfloor.Api;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddMesShopfloorApi();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference("/api/reference", options =>
            {
                options.Title = "MES Shopfloor API Reference";
                options.Agent = new() { Disabled = true };
            });
        }

        app.UseHttpsRedirection();

        app.MapDandyEndpoints();
        app.MapGet("/", () => Results.Redirect("/api/reference"));

        app.Run();
    }
}