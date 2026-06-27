using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Repositories;

internal sealed class ProductionOrderRepository(DbContext _context) : IProductionOrderRepository
{
    public Task<ProductionOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context
            .Set<ProductionOrder>()
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public Task<ProductionOrder?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context
            .Set<ProductionOrder>()
            .Include(p => p.Progress)
            .SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}