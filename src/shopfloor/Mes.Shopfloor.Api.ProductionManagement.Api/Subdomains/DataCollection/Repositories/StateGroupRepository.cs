using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.DataCollection.Repositories;

internal sealed class StateGroupRepository(DbContext _context) : IStateGroupRepository
{
    public Task<StateGroup?> GetByIdAsync(Guid groupId, CancellationToken cancellationToken)
    {
        return _context
            .Set<StateGroup>()
            .SingleOrDefaultAsync(g => g.Id == groupId, cancellationToken);
    }

    public Task<StateGroup?> GetByIdEagerAsync(Guid groupId, CancellationToken cancellationToken)
    {
        return _context
            .Set<StateGroup>()
            .Include(g => g.States)
            .SingleOrDefaultAsync(g => g.Id == groupId, cancellationToken);
    }
}