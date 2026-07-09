using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Scheduling.Repositories;

internal interface IProductionOrderRepository : IRepository
{
    Task<ProductionOrder?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductionOrder?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken);
}