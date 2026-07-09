namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.DataCollection.Factories;

internal interface IProductionUnitStatusFactory
{
    Task<Status?> CreateAsync(Guid productionUnitId, CancellationToken cancellationToken);
}