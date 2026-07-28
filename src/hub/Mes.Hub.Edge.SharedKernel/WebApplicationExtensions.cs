using DandyEndpoints;
using Mes.Library.EntityFrameworkCore;
using Mes.Library.ShopfloorCommands.Hub;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Mes.Hub.Edge.SharedKernel;

public static class WebApplicationExtensions
{
    public static void MapMesHubEdgeSharedKernel(this WebApplication app)
    {
        app.InitializeEfCoreIncludeCache();
        app.MapDandyEndpoints();
        app.MapShopfloorCommandHub();
        app.MapGet("/", () => Results.Redirect("/api/reference"));
    }
}