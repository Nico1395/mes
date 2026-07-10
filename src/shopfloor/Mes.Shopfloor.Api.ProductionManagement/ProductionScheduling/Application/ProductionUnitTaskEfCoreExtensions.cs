using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;

internal static class ProductionUnitTaskEfCoreExtensions
{
    public static IQueryable<ProductionUnitTask> AsEager(this IQueryable<ProductionUnitTask> query, bool eager = true)
    {
        return query.Include(t => t.Order).ThenInclude(o => o!.Progress);
    }

    public static Task<ProductionUnitTask?> GetProductionUnitTaskForProductionUnitAtPointInTimeAsync(
        this DbContext context,
        Guid productionUnitId,
        DateTime pointInTime,
        CancellationToken cancellationToken)
    {
        return context
            .Set<ProductionUnitTask>()
            .Where(t =>  t.ProductionUnitId == productionUnitId && t.StartingAt <= pointInTime && t.CompletingAt >= pointInTime)
            .AsEager()
            .FirstOrDefaultAsync(cancellationToken);
    }
}