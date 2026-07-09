using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.DataCollection.Repositories;

internal interface IRejectGroupRepository : IRepository
{
    Task<RejectGroup?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<RejectGroup?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken);
}
