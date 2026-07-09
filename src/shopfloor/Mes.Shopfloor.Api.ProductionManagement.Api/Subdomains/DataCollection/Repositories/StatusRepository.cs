using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.DataCollection.Repositories;

internal sealed class StatusRepository(DbContext _context) : IStatusRepository
{
    public Task<Status?> GetByIdAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return _context.Set<Status>().SingleOrDefaultAsync(s => s.ProductionUnitId == productionUnitId, cancellationToken);
    }

    public Task<Status?> GetByIdEagerAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return _context
            .Set<Status>()
            .Include(s => s.States)
            .SingleOrDefaultAsync(s => s.ProductionUnitId == productionUnitId, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return _context.Set<Status>().AnyAsync(s => s.ProductionUnitId == productionUnitId, cancellationToken);
    }

    public async Task SaveAsync(Status status, CancellationToken cancellationToken)
    {
        var exists = await ExistsAsync(status.ProductionUnitId, cancellationToken);
        if (exists)
            _context.Update(status);
        else
            _context.Add(status);
    }
}