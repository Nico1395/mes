namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Factories;

internal sealed class ProductionUnitStatusFactory : IProductionUnitStatusFactory
{
    public Task<ProductionUnitStatus?> CreateAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return Task.FromResult<ProductionUnitStatus?>(null);
    }
}