namespace Mes.Shopfloor.Client.ProductionManagement.Resources.Repositories;

internal interface IProductionUnitModelRepository
{
    Task<ProductionUnitModel?> GetByKeyAsync(string key, CancellationToken cancellationToken);
}