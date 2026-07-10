namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinitionManagement.Repositories;

internal interface IProductionProcessModelRepository
{
    Task<ProductionProcessModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}