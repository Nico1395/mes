using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis.Repositories;

internal interface IProductionUnitStatusRepository : IRepository
{
    Task<ProductionUnitStatus?> GetByIdAsync(Guid productionUnitId, CancellationToken cancellationToken);
    Task<ProductionUnitStatus?> GetByIdEagerAsync(Guid productionUnitId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid productionUnitId, CancellationToken cancellationToken);
    Task SaveAsync(ProductionUnitStatus status, CancellationToken cancellationToken);
}