using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Repositories;

internal interface IProductionProcessRepository : IRepository
{
    Task<ProductionProcess?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductionProcess?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken);
}