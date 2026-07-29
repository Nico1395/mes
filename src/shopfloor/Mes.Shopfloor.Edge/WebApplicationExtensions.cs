using Mes.Shopfloor.Edge.SharedKernel;

namespace Mes.Shopfloor.Edge;

internal static class WebApplicationExtensions
{
    public static void MapMesShopfloorEdge(this WebApplication app)
    {
        app.MapMesShopfloorEdgeSharedKernel();
    }
}