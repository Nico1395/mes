using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;

internal static class StatusEfCoreExtensions
{
    public static IQueryable<Status> AsEager(this IQueryable<Status> query, bool eager = true)
    {
        return query.Include(e => e.States);
    }

    public static Task<Status?> GetStatusByProductionUnitIdEagerAsync(this DbContext context, Guid productionUnitId, CancellationToken cancellationToken)
    {
        return context.Set<Status>().AsEager().SingleOrDefaultAsync(o => o.ProductionUnitId == productionUnitId, cancellationToken);
    }
}