using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Repositories;

internal interface IProductionOrderRepository : IRepository
{
    Task<ProductionOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductionOrder?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken);
}