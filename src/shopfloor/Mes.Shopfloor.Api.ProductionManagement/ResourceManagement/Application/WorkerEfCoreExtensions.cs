using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Application;

internal static class WorkerEfCoreExtensions
{
    public static IQueryable<Worker> AsEager(this IQueryable<Worker> query, bool eager = true)
    {
        return query.Include(w => w.Group!)
            .ThenInclude(g => g.Qualifications!)
                .ThenInclude(q => q.WorkerQualification);
    }
}