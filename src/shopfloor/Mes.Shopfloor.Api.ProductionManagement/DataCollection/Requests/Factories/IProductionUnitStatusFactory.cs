namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Requests.Factories;

internal interface IProductionUnitStatusFactory
{
    Task<Status?> CreateAsync(Guid productionUnitId, CancellationToken cancellationToken);
}