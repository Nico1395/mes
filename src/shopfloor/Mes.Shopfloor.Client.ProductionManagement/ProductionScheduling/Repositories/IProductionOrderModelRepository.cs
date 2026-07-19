namespace Mes.Shopfloor.Client.ProductionManagement.ProductionScheduling.Repositories;

internal interface IProductionOrderModelRepository
{
    Task<ProductionOrderModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}