using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Repositories;

internal sealed class RejectGroupRepository(DbContext _context) : IRejectGroupRepository
{
    public Task<RejectGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context
            .Set<RejectGroup>()
            .SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public Task<RejectGroup?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context
            .Set<RejectGroup>()
            .Include(r => r.Rejects)
            .SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
    }
}