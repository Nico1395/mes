using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.DataCollection.Repositories;

internal interface IStatusRepository : IRepository
{
    Task<Status?> GetByIdAsync(Guid productionUnitId, CancellationToken cancellationToken);
    Task<Status?> GetByIdEagerAsync(Guid productionUnitId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid productionUnitId, CancellationToken cancellationToken);
    Task SaveAsync(Status status, CancellationToken cancellationToken);
}