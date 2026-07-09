using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Scheduling.Repositories;

internal sealed class ProductionUnitTaskRepository(DbContext _context) : IProductionUnitTaskRepository
{
    public Task<ProductionUnitTask?> GetTaskForProductionUnitAtPointInTimeAsync(Guid productionUnitId, DateTime pointInTime, CancellationToken cancellationToken)
    {
        return _context
            .Set<ProductionUnitTask>()
            .Where(t =>  t.ProductionUnitId == productionUnitId && t.StartingAt <= pointInTime && t.CompletingAt >= pointInTime)
            .Include(t => t.Order).ThenInclude(o => o!.Progress)
            .FirstOrDefaultAsync(cancellationToken);
    }
}