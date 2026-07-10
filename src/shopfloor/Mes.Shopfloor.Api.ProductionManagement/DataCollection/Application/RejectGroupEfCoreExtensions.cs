using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;

internal static class RejectGroupEfCoreExtensions
{
    public static IQueryable<RejectGroup> AsEager(this IQueryable<RejectGroup> query, bool eager = true)
    {
        return query.Include(e => e.Rejects);
    }

    public static Task<RejectGroup?> GetRejectGroupByIdEagerAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context.Set<RejectGroup>().AsEager().SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
    }
}