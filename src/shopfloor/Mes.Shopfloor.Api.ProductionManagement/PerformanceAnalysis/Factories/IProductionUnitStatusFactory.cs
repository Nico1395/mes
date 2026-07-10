namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Factories;

internal interface IProductionUnitStatusFactory
{
    Task<ProductionUnitStatus?> CreateAsync(Guid productionUnitId, CancellationToken cancellationToken);
}