using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Repositories;

internal interface IStateGroupRepository : IRepository
{
    Task<StateGroup?> GetByIdAsync(Guid groupId, CancellationToken cancellationToken);
    Task<StateGroup?> GetByIdEagerAsync(Guid groupId, CancellationToken cancellationToken);
}