using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.DataCollection.Repositories;

internal sealed class StateRepository(DbContext _context) : IStateRepository
{
    public Task<State?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Set<State>().SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}