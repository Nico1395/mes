using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;

internal static class ProductionOrderEfCoreExtensions
{
    public static Task<ProductionOrder?> GetProductionOrderByIdAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context
            .Set<ProductionOrder>()
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}