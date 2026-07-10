using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Scheduling.Application;

internal static class ProductionOrderEfCoreExtensions
{
    public static IQueryable<ProductionOrder> AsEager(this IQueryable<ProductionOrder> query, bool eager = true)
    {
        return query.Include(p => p.Progress);
    }

    public static Task<ProductionOrder?> GetProductionOrderByIdEagerAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context
            .Set<ProductionOrder>()
            .AsEager()
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}