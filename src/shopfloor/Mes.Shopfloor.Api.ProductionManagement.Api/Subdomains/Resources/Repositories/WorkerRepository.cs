using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources.Repositories;

internal sealed class WorkerRepository(DbContext _context) : IWorkerRepository
{
    public Task<Worker?> GetByNumberAsync(string number, CancellationToken cancellationToken)
    {
        return _context
            .Set<Worker>()
            .SingleOrDefaultAsync(t => t.Number == number, cancellationToken);
    }

    public Task<Worker?> GetByNumberEagerAsync(string number, CancellationToken cancellationToken)
    {
        return _context
            .Set<Worker>()
            .Include(w => w.Group!)
            .ThenInclude(g => g.Qualifications!)
            .ThenInclude(q => q.WorkerQualification)
            .SingleOrDefaultAsync(t => t.Number == number, cancellationToken);
    }
}