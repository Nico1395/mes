using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Scheduling.Application;

internal static class ProductionUnitScheduleEfCoreExtensions
{
    public static IQueryable<ProductionUnitSchedule> AsEager(this IQueryable<ProductionUnitSchedule> query, bool eager = true)
    {
        return query.Include(p => p.Tasks);
    }

    public static Task<ProductionUnitSchedule?> GetProductionUnitScheduleForProductionUnitAsync(this DbContext context, Guid productionUnitId, CancellationToken cancellationToken)
    {
        return context
            .Set<ProductionUnitSchedule>()
            .SingleOrDefaultAsync(e => e.ProductionUnitId == productionUnitId, cancellationToken);
    }

    public static Task<ProductionUnitSchedule?> GetProductionUnitScheduleForProductionEagerUnitAsync(this DbContext context, Guid productionUnitId, CancellationToken cancellationToken)
    {
        return context.Set<ProductionUnitSchedule>()
            .AsEager()
            .SingleOrDefaultAsync(e => e.ProductionUnitId == productionUnitId, cancellationToken);
    }
}