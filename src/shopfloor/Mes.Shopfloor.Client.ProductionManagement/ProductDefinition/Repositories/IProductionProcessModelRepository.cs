namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition.Repositories;

internal interface IProductionProcessModelRepository
{
    Task<ProductionProcessModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}