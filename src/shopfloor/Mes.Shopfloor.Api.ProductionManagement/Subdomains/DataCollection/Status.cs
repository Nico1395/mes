namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection;

internal sealed class Status
{
    public required Guid ProductionUnitId { get; init; }
    public List<StatusState> States { get; init; } = [];

    public void SetState(StatusState state)
    {
        var lastState = States.OrderBy(s => s.StartedAt).LastOrDefault();
        if (lastState != null)
        {
            // Dismiss the new state if already stale
            if (lastState.StartedAt > state.StartedAt)
                return;

            lastState.End(state.StartedAt);
        }

        States.Add(state);
    }
}
