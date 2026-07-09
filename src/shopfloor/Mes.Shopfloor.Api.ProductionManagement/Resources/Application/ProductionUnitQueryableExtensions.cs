using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Application;

internal static class ProductionUnitQueryableExtensions
{
    public static IQueryable<ProductionUnit> AsEager(this IQueryable<ProductionUnit> query, bool eager = true)
    {
        return query
            .Include(p => p.Type)
            .Include(p => p.Group!)
                .ThenInclude(g => g.RequiredQualifications!)
                    .ThenInclude(p => p.WorkerQualification);
    }
}