namespace Mes.Shopfloor.Terminal.Core.Domains.ProductionManagement.Subdomains.Resources.Manager;

internal interface IProductionUnitModelManager
{
    Task<ProductionUnitModel?> GetCurrentAsync(CancellationToken cancellationToken);
}