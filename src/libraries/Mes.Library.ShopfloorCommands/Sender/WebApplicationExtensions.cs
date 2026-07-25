using Microsoft.AspNetCore.Builder;

namespace Mes.Library.ShopfloorCommands.Sender;

public static class WebApplicationExtensions
{
    public static void MapShopfloorCommandHub(this WebApplication app)
    {
        app.MapHub<ShopfloorCommandHubV1>("/cmd/");
    }
}