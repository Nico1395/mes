using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Repositories;

internal sealed class ProductionProcessRepository(DbContext _context) : IProductionProcessRepository
{
    public Task<ProductionProcess?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context
            .Set<ProductionProcess>()
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task<ProductionProcess?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context
            .Set<ProductionProcess>()
            .Include(p => p.Steps!).ThenInclude(s => s.Parts!).ThenInclude(p => p.Part)
            .Include(p => p.Steps!).ThenInclude(s => s.Material!).ThenInclude(p => p.Material)
            .Include(p => p.Steps!).ThenInclude(s => s.Equipment!).ThenInclude(p => p.Equipment)
            .Include(p => p.Steps!).ThenInclude(s => s.Parameters)
            .SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}