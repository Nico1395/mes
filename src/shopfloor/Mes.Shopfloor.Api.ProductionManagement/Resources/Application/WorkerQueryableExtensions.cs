using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Application;

internal static class WorkerQueryableExtensions
{
    public static IQueryable<Worker> AsEager(this IQueryable<Worker> query, bool eager = true)
    {
        return query.Include(w => w.Group!)
            .ThenInclude(g => g.Qualifications!)
                .ThenInclude(q => q.WorkerQualification);
    }
}