using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Repositories;

internal interface IRejectGroupRepository : IRepository
{
    Task<RejectGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<RejectGroup?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken);
}
