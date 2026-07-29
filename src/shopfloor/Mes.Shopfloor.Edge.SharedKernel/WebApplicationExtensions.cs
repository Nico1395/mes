using DandyEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Mes.Shopfloor.Edge.SharedKernel;

public static class WebApplicationExtensions
{
    public static void MapMesShopfloorEdgeSharedKernel(this WebApplication app)
    {
        app.MapDandyEndpoints();
        app.MapGet("/", () => Results.Redirect("/api/reference"));
    }
}