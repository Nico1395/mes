using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;

internal static class ProductionOrderScheduleEfCoreExtensions
{
    public static IQueryable<ScheduledProductionOrder> AsEager(this IQueryable<ScheduledProductionOrder> query, bool eager = true)
    {
        if (!eager)
            return query;

        return query.Include(q => q.Tasks!).ThenInclude(t => t.Workers);
    }
    
    public static Task<ScheduledProductionOrder?> GetScheduledProductionOrderByOrderIdEagerAsync(this DbContext context, Guid orderId, CancellationToken cancellationToken)
    {
        return context.Set<ScheduledProductionOrder>().AsEager().SingleOrDefaultAsync(p => p.ProductionOrderId == orderId, cancellationToken);
    }

    public static Task<ScheduledProductionOrder?> GetScheduledProductionOrderByIdAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context.Set<ScheduledProductionOrder>().AsEager().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}