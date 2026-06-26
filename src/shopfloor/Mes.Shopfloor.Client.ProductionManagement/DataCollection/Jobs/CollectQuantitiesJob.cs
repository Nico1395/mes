using Mes.Shopfloor.Client.Infrastructure.Routine;

namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Jobs;

internal sealed class CollectQuantitiesJob : RoutineJob
{
    private static readonly Random _random = new();

    private int _producedQuantity;
    private int _rejectQuantity;

    public override int Order => RoutineJobOrder.CollectQuantities.ToInt();

    public override Task ExecuteAsync(IRoutineContext context, CancellationToken cancellationToken)
    {
        // Usually you would request quantity data from either
        // 1. a OPC UA server,
        // 2. some kind of I/O-Box or
        // 3. maybe an internal PCB that caches produced quantities.
        // For now however we are just generating some numbers.

        _producedQuantity = NextProducedQuantity();
        _rejectQuantity = NextRejectQuantity();

        return Task.CompletedTask;
    }

    public override void Synchronize(IRoutineContext context)
    {
        context.Set(RoutineDataKey.ProducedQuantity, _producedQuantity);
        context.Set(RoutineDataKey.RejectQuantity, _rejectQuantity);
    }

    private static int NextProducedQuantity()
    {
        return _random.Next(3, 10);
    }

    private static int NextRejectQuantity()
    {
        return _random.Next(0, 1);
    }
}