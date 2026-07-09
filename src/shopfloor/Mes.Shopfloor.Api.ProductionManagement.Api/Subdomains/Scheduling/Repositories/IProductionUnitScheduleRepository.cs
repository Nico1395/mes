using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Scheduling.Repositories;

internal interface IProductionUnitScheduleRepository : IRepository
{
    Task<ProductionUnitSchedule?> GetForProductionUnitAsync(Guid productionUnitId, CancellationToken cancellationToken);
}