namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis.Factories;

internal interface IProductionUnitStatusFactory
{
    Task<ProductionUnitStatus?> CreateAsync(Guid productionUnitId, CancellationToken cancellationToken);
}