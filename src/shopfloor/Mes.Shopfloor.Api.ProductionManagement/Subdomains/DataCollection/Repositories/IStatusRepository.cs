using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Repositories;

internal interface IStatusRepository : IRepository
{
    Task<Status?> GetByIdAsync(Guid productionUnitId, CancellationToken cancellationToken);
    Task<Status?> GetByIdEagerAsync(Guid productionUnitId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid productionUnitId, CancellationToken cancellationToken);
    Task SaveAsync(Status status, CancellationToken cancellationToken);
}