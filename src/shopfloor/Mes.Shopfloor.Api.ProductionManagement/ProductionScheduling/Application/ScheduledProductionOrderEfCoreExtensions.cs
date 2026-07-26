using Mes.Library.Domain.Abstractions.Graphs;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;

internal static class ScheduledProductionOrderEfCoreExtensions
{
    public static async Task<ScheduledOrder?> GetScheduledProductionOrderByOrderIdAsync(this DbContext context, Guid orderId, CancellationToken cancellationToken)
    {
        var scheduledOrders = await context
            .Set<ScheduledOrder>()
            .Where(s => s.OrderId == orderId)
            .Include(o => o.Edges)
            .Include(o => o.Material)
            .Include(o => o.Parameters)
            .Include(o => o.Parts)
            .Include(o => o.Equipment)
            .ToDictionaryAsync(o => o.Id, cancellationToken);

        return scheduledOrders.ToGraph();
    }
}