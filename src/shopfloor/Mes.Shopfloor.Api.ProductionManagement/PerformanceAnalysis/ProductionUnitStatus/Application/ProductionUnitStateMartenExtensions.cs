using Marten;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionUnitStatus.Application;

internal static class ProductionUnitStateMartenExtensions
{
    public static Task<ProductionUnitStatus?> GetProductionUnitStatusByIdAsync(this IDocumentSession session, Guid id, CancellationToken cancellationToken)
    {
        return session.Query<ProductionUnitStatus>().SingleOrDefaultAsync(p => p.ProductionUnitId == id, cancellationToken);
    }
}