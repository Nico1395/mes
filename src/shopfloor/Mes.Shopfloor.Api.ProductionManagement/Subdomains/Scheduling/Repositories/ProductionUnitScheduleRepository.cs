using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Repositories;

internal sealed class ProductionUnitScheduleRepository(DbContext _context) : IProductionUnitScheduleRepository
{
    public Task<ProductionUnitSchedule?> GetForProductionUnitAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return _context.Set<ProductionUnitSchedule>().SingleOrDefaultAsync(e => e.ProductionUnitId == productionUnitId, cancellationToken);
    }
}