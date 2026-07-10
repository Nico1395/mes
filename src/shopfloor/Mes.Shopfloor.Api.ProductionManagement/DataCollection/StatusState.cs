namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection;

internal sealed class StatusState
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionUnitId { get; init; }
    public required Guid StateId { get; init; }
    public State? State { get; init; }
    public required DateTime StartedAt { get; init; }
    public DateTime? EndedAt { get; private set; }

    public static StatusState FromState(Guid productionUnitId, State state, DateTime startedAt)
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