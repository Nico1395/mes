using Mes.Shopfloor.Api.ProductionManagement.DataCollection;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis;

internal sealed class ProductionUnitStatusState
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionUnitId { get; init; }
    public required Guid StateId { get; init; }
    public ProductionUnitState? State { get; init; }
    public required DateTime StartedAt { get; init; }
    public DateTime? EndedAt { get; private set; }

    public static ProductionUnitStatusState FromState(Guid productionUnitId, ProductionUnitState productionUnitState, DateTime startedAt)
    {
        return new()
        {
            ProductionUnitId = productionUnitId,
            StateId =  productionUnitState.Id,
            State =  productionUnitState,
            StartedAt = startedAt,
        };
    }

    public TimeSpan GetDuration()
    {
        var end = EndedAt ?? DateTime.UtcNow;
        return end - StartedAt;
    }

    public void End(DateTime endedAt)
    {
        EndedAt = endedAt;
    }
}