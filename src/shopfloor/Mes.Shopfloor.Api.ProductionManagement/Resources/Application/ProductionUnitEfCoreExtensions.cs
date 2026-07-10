using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Application;

internal static class ProductionUnitEfCoreExtensions
{
    public static IQueryable<ProductionUnit> AsEager(this IQueryable<ProductionUnit> query, bool eager = true)
    {
        return query
            .Include(p => p.Type)
            .Include(p => p.Group!)
                .ThenInclude(g => g.RequiredQualifications!)
                    .ThenInclude(p => p.WorkerQualification);
    }

    public static Task<bool> ProductionUnitExistsAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context.Set<ProductionUnit>().AnyAsync(p => p.Id == id, cancellationToken);
    }

    public static Task<ProductionUnit?> GetProductionUnitByIdEagerAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context.Set<ProductionUnit>().AsEager().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public static Task<ProductionUnit?> GetProductionUnitByKeyEagerAsync(this DbContext context, string key, CancellationToken cancellationToken)
    {
        return context.Set<ProductionUnit>().AsEager().SingleOrDefaultAsync(p => p.Key == key, cancellationToken);
    }
}