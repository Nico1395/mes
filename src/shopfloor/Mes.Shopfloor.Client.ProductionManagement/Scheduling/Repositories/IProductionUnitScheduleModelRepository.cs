namespace Mes.Shopfloor.Client.ProductionManagement.Scheduling.Repositories;

internal interface IProductionUnitScheduleModelRepository
{
    Task<ProductionUnitScheduleModel?> GetByProductionUnitIdAsync(Guid productionUnitId, CancellationToken cancellationToken);
}