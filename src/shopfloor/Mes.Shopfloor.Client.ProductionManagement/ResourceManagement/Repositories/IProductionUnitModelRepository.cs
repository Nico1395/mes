namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement.Repositories;

internal interface IProductionUnitModelRepository
{
    Task<ProductionUnitModel?> GetByKeyAsync(string key, CancellationToken cancellationToken);
}