using System.Diagnostics.CodeAnalysis;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis;

internal sealed class ProductionUnitStatusState
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid StateId { get; init; }
    public ProductionUnitState? State { get; init; }
    public required DateTime StartedAt { get; init; }
    public DateTime? EndedAt { get; private set; }

    public static ProductionUnitStatusState FromState(Guid productionUnitId, ProductionUnitState state, DateTime startedAt)
    {
        return new()
        {
            ProductionUnitId = productionUnitId,
            StateId =  state.Id,
            State =  state,
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