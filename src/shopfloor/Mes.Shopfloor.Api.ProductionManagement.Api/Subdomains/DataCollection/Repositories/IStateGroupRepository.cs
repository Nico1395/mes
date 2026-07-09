using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.DataCollection.Repositories;

internal interface IStateGroupRepository : IRepository
{
    Task<StateGroup?> GetByIdAsync(Guid groupId, CancellationToken cancellationToken);
    Task<StateGroup?> GetByIdEagerAsync(Guid groupId, CancellationToken cancellationToken);
}