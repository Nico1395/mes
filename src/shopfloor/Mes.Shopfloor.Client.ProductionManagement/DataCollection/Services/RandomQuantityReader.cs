namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Services;

internal sealed class RandomQuantityReader : IQuantityReader
{
    private static readonly Random _random = new();

    public Task<int> ReadQuantityAsync(CancellationToken cancellationToken)
    {
        Thread.Sleep(0); // Simulate some kind of response time from some quantity source (this is a bad scenario).
        var quantity = _random.Next(3, 10);

        return Task.FromResult(quantity);
    }
}