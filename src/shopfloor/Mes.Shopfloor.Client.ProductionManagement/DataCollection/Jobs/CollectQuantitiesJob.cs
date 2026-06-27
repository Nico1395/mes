using Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;

namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Jobs;

internal sealed class CollectQuantitiesJob : TerminalRoutineJob
{
    private static readonly Random _random = new();

    private int _producedQuantity;
    private int _rejectQuantity;

    public override int Order => JobOrder.CollectQuantities.ToInt();

    public override Task ExecuteAsync(ITerminalRoutineContext context, CancellationToken cancellationToken)
    {
        // Usually you would request quantity data from either
        // 1. a OPC UA server,
        // 2. some kind of I/O-Box or
        // 3. maybe an internal PCB that caches produced quantities.
        // For now however we are just generating some numbers.

        _producedQuantity = NextProducedQuantity();
        _rejectQuantity = NextRejectQuantity(); // We should probably remove this

        return Task.CompletedTask;
    }

    public override void Synchronize(ITerminalRoutineContext context)
    {
        context.Set(DataKey.ProducedQuantity, _producedQuantity);
        context.Set(DataKey.RejectQuantity, _rejectQuantity);
    }

    private static int NextProducedQuantity()
    {
        Thread.Sleep(10); // Simulate some kind of response time from some quantity source (this is a bad scenario).
        return _random.Next(3, 10);
    }

    private static int NextRejectQuantity()
    {
        return _random.Next(0, 1);
    }
}