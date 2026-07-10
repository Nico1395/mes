using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;

internal static class StatusEfCoreExtensions
{
    public static IQueryable<ProductionUnitStatus> AsEager(this IQueryable<ProductionUnitStatus> query, bool eager = true)
    {
        return query.Include(e => e.States);
    }

    public static Task<ProductionUnitStatus?> GetStatusByProductionUnitIdEagerAsync(this DbContext context, Guid productionUnitId, CancellationToken cancellationToken)
    {
        return context.Set<ProductionUnitStatus>().AsEager().SingleOrDefaultAsync(o => o.ProductionUnitId == productionUnitId, cancellationToken);
    }
}