using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources.Repositories;

internal interface IProductionUnitRepository : IRepository
{
    Task<ProductionUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductionUnit?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductionUnit?> GetByKeyAsync(string key, CancellationToken cancellationToken);
    Task<ProductionUnit?> GetByKeyEagerAsync(string key, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}