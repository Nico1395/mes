namespace Mes.Shopfloor.Client.ProductionManagement.ProductionScheduling.Repositories;

internal interface IProductionUnitScheduleModelRepository
{
    Task<ProductionUnitScheduleModel?> GetByProductionUnitIdAsync(Guid productionUnitId, CancellationToken cancellationToken);
}