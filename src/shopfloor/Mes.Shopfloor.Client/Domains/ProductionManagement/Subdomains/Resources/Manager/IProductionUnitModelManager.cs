namespace Mes.Shopfloor.Client.Domains.ProductionManagement.Subdomains.Resources.Manager;

internal interface IProductionUnitModelManager
{
    Task<ProductionUnitModel?> GetCurrentAsync(CancellationToken cancellationToken);
}