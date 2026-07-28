using Mes.Hub.Edge.Synchronization.MasterData.Infrastructure;
using Microsoft.AspNetCore.Builder;

namespace Mes.Hub.Edge.Synchronization;

public static class WebApplicationExtensions
{
    public static void MapMesHubEdgeSynchronization(this WebApplication app)
    {
        app.MapMasterDataHub();
    }
}