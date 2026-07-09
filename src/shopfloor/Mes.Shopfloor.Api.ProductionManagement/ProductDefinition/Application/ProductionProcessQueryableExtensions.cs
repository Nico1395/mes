using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Application;

internal static class ProductionProcessQueryableExtensions
{
    public static IQueryable<ProductionProcess> AsEager(this IQueryable<ProductionProcess> query, bool eager = true)
    {
        if (!eager)
            return query;
        
        return query
            .Include(p => p.Steps!).ThenInclude(s => s.Parts!).ThenInclude(p => p.Part)
            .Include(p => p.Steps!).ThenInclude(s => s.Material!).ThenInclude(p => p.Material)
            .Include(p => p.Steps!).ThenInclude(s => s.Parameters);
    }
}