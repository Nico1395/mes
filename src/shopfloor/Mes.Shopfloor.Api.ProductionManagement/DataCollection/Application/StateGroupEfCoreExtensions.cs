using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;

internal static class StateGroupEfCoreExtensions
{
    public static IQueryable<StateGroup> AsEager(this IQueryable<StateGroup> query, bool eager = true)
    {
        return query.Include(e => e.States);
    }

    public static Task<StateGroup?> GetStateGroupByIdEagerAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context.Set<StateGroup>().AsEager().SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
    }
}