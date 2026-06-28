namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Services;

public interface IQuantityReader
{
    Task<int> ReadQuantityAsync(CancellationToken cancellationToken);
}