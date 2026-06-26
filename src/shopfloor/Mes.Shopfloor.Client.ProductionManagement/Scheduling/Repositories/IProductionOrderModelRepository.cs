namespace Mes.Shopfloor.Client.ProductionManagement.Scheduling.Repositories;

internal interface IProductionOrderModelRepository
{
    Task<ProductionOrderModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}