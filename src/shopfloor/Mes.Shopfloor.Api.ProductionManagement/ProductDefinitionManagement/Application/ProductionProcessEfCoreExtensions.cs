using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Application;

internal static class ProductionProcessEfCoreExtensions
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

    public static Task<ProductionProcess?> GetProductionProcessByIdEagerAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context
            .Set<ProductionProcess>()
            .AsEager()
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}