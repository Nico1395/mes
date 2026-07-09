using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.ProductDefinition.Repositories;

internal interface IProductionProcessRepository : IRepository
{
    Task<ProductionProcess?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ProductionProcess?> GetByIdEagerAsync(Guid id, CancellationToken cancellationToken);
}