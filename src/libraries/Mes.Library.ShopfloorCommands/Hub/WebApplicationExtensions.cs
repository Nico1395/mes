using Microsoft.AspNetCore.Builder;

namespace Mes.Library.ShopfloorCommands.Hub;

public static class WebApplicationExtensions
{
    public static void MapShopfloorCommandHub(this WebApplication app)
    {
        app.MapHub<ShopfloorCommandHub>("/cmd/");
    }
}