using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Repositories;

internal interface IProductionUnitTaskRepository : IRepository
{
    Task<ProductionUnitTask?> GetTaskForProductionUnitAtPointInTimeAsync(Guid productionUnitId, DateTime pointInTime, CancellationToken cancellationToken);
}