using Marten;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Application;

internal static class ProductionOrderStateMartenExtensions
{
    public static Task<ProductionOrderStatus?> GetProductionOrderStatusByIdAsync(this IDocumentSession session, Guid id, CancellationToken cancellationToken)
    {
        return session.Query<ProductionOrderStatus>().SingleOrDefaultAsync(p => p.ProductionOrderId == id, cancellationToken);
    }
}