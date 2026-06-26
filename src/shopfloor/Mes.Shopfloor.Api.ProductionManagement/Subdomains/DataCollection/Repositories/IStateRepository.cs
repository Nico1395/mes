using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Repositories;

internal interface IStateRepository : IRepository
{
    Task<State?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}