namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Analysis;

internal sealed class ProductionUnitStatus
{
    public required Guid ProductionUnitId { get; init; }
    public List<ProductionUnitStatusState> States { get; init; } = [];

    public void SetState(ProductionUnitStatusState state)
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
