using Mes.Shopfloor.Client.Configuration;
using Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;
using Mes.Shopfloor.Client.ProductionManagement.DataCollection.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Jobs;

internal sealed class CollectQuantitiesJob(
    IOptions<ProductionUnitOptions> _options,
    IServiceProvider _serviceProvider) : TerminalRoutineJob
{
    private static readonly Random _random = new();

    private int _producedQuantity;
    private int _rejectQuantity;

    public override int Order => JobOrder.CollectQuantities.ToInt();

    public override async Task ExecuteAsync(ITerminalRoutineContext context, CancellationToken cancellationToken)
    {
        // Usually you would request quantity data from either
        // 1. a OPC UA server,
        // 2. some kind of I/O-Box or
        // 3. maybe an internal PCB that caches produced quantities.
        // And also quantities might be reported as tacts and how many pieces have been produced per tact should be configurable.
        // For now however we are just generating some numbers.

        var reader = _serviceProvider.GetRequiredKeyedService<IQuantityReader>(_options.Value.QuantitySource);
        var quantity = await reader.ReadQuantityAsync(cancellationToken);

        _producedQuantity = quantity;
        _rejectQuantity = NextRejectQuantity(); // We should probably remove this
    }

    public override void Synchronize(ITerminalRoutineContext context)
    {
        context.Set(DataKey.ProducedQuantity, _producedQuantity);
        context.Set(DataKey.RejectQuantity, _rejectQuantity);
    }

    private static int NextRejectQuantity()
    {
        return _random.Next(0, 1);
    }
}