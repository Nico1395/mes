using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis.Repositories;

internal interface IProductionUnitStateRepository : IRepository
{
    Task<ProductionUnitState?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}