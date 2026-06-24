using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis.Repositories;

internal sealed class ProductionUnitStatusRepository(DbContext _context) : IProductionUnitStatusRepository
{
    public Task<ProductionUnitStatus?> GetByIdAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return _context.Set<ProductionUnitStatus>().SingleOrDefaultAsync(s => s.ProductionUnitId == productionUnitId, cancellationToken);
    }

    public Task<ProductionUnitStatus?> GetByIdEagerAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return _context
            .Set<ProductionUnitStatus>()
            .Include(s => s.States)
            .SingleOrDefaultAsync(s => s.ProductionUnitId == productionUnitId, cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return _context.Set<ProductionUnitStatus>().AnyAsync(s => s.ProductionUnitId == productionUnitId, cancellationToken);
    }

    public async Task SaveAsync(ProductionUnitStatus status, CancellationToken cancellationToken)
    {
        var exists = await ExistsAsync(status.ProductionUnitId, cancellationToken);
        if (exists)
            _context.Update(status);
        else
            _context.Add(status);
    }
}