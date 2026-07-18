using Marten;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderStatus.Application;

internal static class ProductionOrderStateMartenExtensions
{
    public static Task<ProductionOrderStatus?> GetProductionOrderStatusByIdAsync(this IDocumentSession session, Guid id, CancellationToken cancellationToken)
    {
        return session.Query<ProductionOrderStatus>().SingleOrDefaultAsync(p => p.ProductionOrderId == id, cancellationToken);
    }
}