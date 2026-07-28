using Mes.Hub.Edge.SharedKernel;
using Mes.Hub.Edge.Synchronization;

namespace Mes.Hub.Edge;

internal static class WebApplicationExtensions
{
    public static void MapHubEdge(this WebApplication app)
    {
        app.MapMesHubEdgeSharedKernel();
        app.MapMesHubEdgeSynchronization();
    }
}