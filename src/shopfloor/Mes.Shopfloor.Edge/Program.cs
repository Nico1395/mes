using DandyEndpoints;
using Scalar.AspNetCore;

namespace Mes.Shopfloor.Edge;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddMesShopfloorEdge(builder.Configuration);

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference("/api/reference", options =>
            {
                options.Title = "MES Shopfloor Edge Reference";
                options.Agent = new() { Disabled = true };
            });
        }

        app.UseHttpsRedirection();

        app.MapDandyEndpoints();

        app.Run();
    }
}