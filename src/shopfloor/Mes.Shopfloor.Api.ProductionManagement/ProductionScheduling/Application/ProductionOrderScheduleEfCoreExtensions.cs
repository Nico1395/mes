using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;

internal static class ProductionOrderScheduleEfCoreExtensions
{
    public static IQueryable<ProductionOrderSchedule> AsEager(this IQueryable<ProductionOrderSchedule> query, bool eager = true)
    {
        if (!eager)
            return query;

        return query.Include(q => q.Tasks!).ThenInclude(t => t.Workers);
    }
    
    public static Task<ProductionOrderSchedule?> GetProductionOrderScheduleByOrderIdEagerAsync(this DbContext context, Guid orderId, CancellationToken cancellationToken)
    {
        return context.Set<ProductionOrderSchedule>().AsEager().SingleOrDefaultAsync(p => p.ProductionOrderId == orderId, cancellationToken);
    }
}