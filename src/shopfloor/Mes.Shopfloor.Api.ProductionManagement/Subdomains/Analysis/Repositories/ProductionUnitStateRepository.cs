using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis.Repositories;

internal sealed class ProductionUnitStateRepository(DbContext _context) : IProductionUnitStateRepository
{
    public Task<ProductionUnitState?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Set<ProductionUnitState>().SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
    }
}