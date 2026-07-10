using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;

internal static class ProductionUnitStateEfCoreExtensions
{
    public static Task<ProductionUnitState?> GetProductionUnitStateByIdAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context.Set<ProductionUnitState>().SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
    }
}