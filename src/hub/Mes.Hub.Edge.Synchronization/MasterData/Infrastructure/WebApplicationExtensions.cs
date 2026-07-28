using Microsoft.AspNetCore.Builder;

namespace Mes.Hub.Edge.Synchronization.MasterData.Infrastructure;

public static class WebApplicationExtensions
{
    public static void MapMasterDataHub(this WebApplication app)
    {
        app.MapHub<MasterDataHub>("/api/v1/synchronization/master-data/push");
    }
}