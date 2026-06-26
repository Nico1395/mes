using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Repositories;

internal interface IProductionUnitScheduleRepository : IRepository
{
    Task<ProductionUnitSchedule?> GetForProductionUnitAsync(Guid productionUnitId, CancellationToken cancellationToken);
}