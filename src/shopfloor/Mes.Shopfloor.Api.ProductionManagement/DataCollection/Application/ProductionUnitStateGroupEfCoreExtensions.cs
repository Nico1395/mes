using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;

internal static class ProductionUnitStateGroupEfCoreExtensions
{
    public static IQueryable<ProductionUnitStateGroup> AsEager(this IQueryable<ProductionUnitStateGroup> query, bool eager = true)
    {
        return query.Include(e => e.States);
    }

    public static Task<ProductionUnitStateGroup?> GetProductionUnitStateGroupByIdEagerAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context.Set<ProductionUnitStateGroup>().AsEager().SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
    }
}