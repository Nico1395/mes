using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.DataCollection.Repositories;

internal interface IStateRepository : IRepository
{
    Task<State?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}