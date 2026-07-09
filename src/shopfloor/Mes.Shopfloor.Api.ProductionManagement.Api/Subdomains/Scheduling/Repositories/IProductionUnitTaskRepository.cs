using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Scheduling.Repositories;

internal interface IProductionUnitTaskRepository : IRepository
{
    Task<ProductionUnitTask?> GetTaskForProductionUnitAtPointInTimeAsync(Guid productionUnitId, DateTime pointInTime, CancellationToken cancellationToken);
}