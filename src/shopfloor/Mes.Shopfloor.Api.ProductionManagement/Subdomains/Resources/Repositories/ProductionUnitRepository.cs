using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Repositories;

internal sealed class ProductionUnitRepository(DbContext _context) : IProductionUnitRepository
{
    public Task<ProductionUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Set<ProductionUnit>().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task<ProductionUnit?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context
            .Set<ProductionUnit>()
            .Include(p => p.Group)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task<ProductionUnit?> GetByKeyAsync(string key, CancellationToken cancellationToken)
    {
        return _context.Set<ProductionUnit>().SingleOrDefaultAsync(p => p.Key == key, cancellationToken);
    }

    public Task<ProductionUnit?> GetByKeyEagerAsync(string key, CancellationToken cancellationToken)
    {
        return _context
            .Set<ProductionUnit>()
            .Include(p => p.Group)
            .SingleOrDefaultAsync(p => p.Key == key, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Set<ProductionUnit>().AnyAsync(p => p.Id == id, cancellationToken);
    }
}