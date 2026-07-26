using Mes.Library.Domain.Abstractions.Durational;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionUnitAnalysis;

internal class ProductionUnitStatus
{
    public Guid ProductionUnitId { get; init; }
    public int Version { get; set; }
    public Guid? ProductionOrderId { get; set; }
    public Guid? ScheduledTaskId { get; set; }
    public List<ProductionUnitStatusState> States { get; init; } = [];
    public List<ProductionUnitProducedQuantity> ProducedQuantities { get; init; } = [];
    public List<ProductionUnitProducedReject> ProducedRejectQuantities { get; init; } = [];
    public List<ProductionUnitMaterialConsumption> MaterialConsumption { get; init; } = [];
    public List<ProductionUnitPartsConsumption> PartConsumption { get; init; } = [];
    public List<ProductionUnitStatusWorker> Workers { get; init; } = [];
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public void BookOrder(Guid productionOrderId, Guid scheduledTaskId)
    {
        // Needs to be idempotent because this event is likely published once for the current and previous order each
        if (ProductionOrderId == productionOrderId && ScheduledTaskId == scheduledTaskId)
            return;

        ProductionOrderId = productionOrderId;
        ScheduledTaskId = scheduledTaskId;

        Touch();
    }

    public void SetState(Guid stateId, bool isProductive, bool isIdle, DateTime startedAt)
    {
        var mostRecentState = GetMostRecentState();
        // if (currentState != null && currentState.WorkerId != workerId)
        // {
        //     // Reminder for later -> If the current worker ID and the new worker ID differ, the app could internally publish a domain event about the workers changing.
        // }

        var newState = new ProductionUnitStatusState
        {
            ProductionUnitId = ProductionUnitId,
            IsProductive = isProductive,
            IsIdle = isIdle,
            StateId = stateId,
            StartedAt = startedAt,
        };

        // Either
        // (1) the new state is the first state, or
        // (2) the current state has not ended yet and needs to be ended by the new state, or
        // (3) the current state has ended and the new state started afterward
        if (mostRecentState == null || !mostRecentState.HasEnded() || newState.StartedAfter(mostRecentState))
        {
            mostRecentState?.TouchEndedAt(newState.StartedAt); // End the current state
            States.Add(newState); // Append the new state, making it the current one
        }
    }

    public ProductionUnitStatusState? GetMostRecentState()
    {
        return States.LastOrDefault();
    }

    public TimeSpan GetProductiveTimeBetween(DateTime start, DateTime end)
    {
        // TODO -> The time the production unit is idling should not be part of the total time
        // TODO -> Check whether this can be simplified by summing up the time of productive states and simply do prodTime / totalTime

        var totalProductiveTime = TimeSpan.Zero;
        if (start >= end)
            return totalProductiveTime;

        // Productive states that between start and end (including states that are on-boundary)
        var productiveStates = States.WhereBetweenAndOn(start, end, d => d.IsProductive);
        var now = DateTime.UtcNow;

        foreach (var state in productiveStates)
        {
            TimeSpan productiveTime;

            if (start.LiesIn(state)) // State started earlier and ended after the start (or has not ended yet)
            {
                productiveTime = (state.EndedAt ?? now) - start;
            }
            else if (end.LiesIn(state)) // State started earlier and ended after the end (or has not ended yet)
            {
                productiveTime = end - state.StartedAt;
            }
            else
            {
                productiveTime = state.GetDurationUntilEndOrCurrent();
            }

            totalProductiveTime += productiveTime;
        }

        return totalProductiveTime;
    }

    public TimeSpan GetDowntimeBetween(DateTime start, DateTime end)
    {
        if (start >= end)
            return TimeSpan.Zero;

        var duration = end - start;
        var totalProductiveTime = GetProductiveTimeBetween(start, end);

        return duration - totalProductiveTime;
    }

    public double GetProductiveTimePercentage(DateTime start, DateTime end)
    {
        if (start >= end)
            return 0;

        var duration = end - start;
        var productiveTime = GetProductiveTimeBetween(start, end);

        return productiveTime / duration;
    }

    public double GetDowntimePercentage(DateTime start, DateTime end)
    {
        if (start >= end)
            return 0;

        var duration = end - start;
        var downtime = GetDowntimeBetween(start, end);

        return downtime / duration;
    }

    public bool TrySetWorker(Guid workerId, DateTime start)
    {
        var mostRecentWorker = GetMostRecentWorker();
        var newWorker = new ProductionUnitStatusWorker
        {
            ProductionUnitId = ProductionUnitId,
            WorkerId = workerId,
            StartedAt = start,
        };

        if (mostRecentWorker == null || !mostRecentWorker.HasEnded() || newWorker.StartedAfter(mostRecentWorker))
        {
            mostRecentWorker?.TouchEndedAt(newWorker.StartedAt);
            Workers.Add(newWorker);

            return true;
        }

        return false;
    }

    public ProductionUnitStatusWorker? GetMostRecentWorker()
    {
        return Workers.LastOrDefault();
    }

    internal void Touch() => UpdatedAt = DateTime.UtcNow;
}