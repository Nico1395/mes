namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Factories;

internal interface IProductionUnitStatusFactory
{
    Task<Status?> CreateAsync(Guid productionUnitId, CancellationToken cancellationToken);
}