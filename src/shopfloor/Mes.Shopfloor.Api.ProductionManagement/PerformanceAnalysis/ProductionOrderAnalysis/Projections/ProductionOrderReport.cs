namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Projections;

public class ProductionOrderReport
{
    public Guid ProductionOrderId { get; init; }
    public Guid ScheduledProductionOrderId { get; init; }
    public Guid ProductId { get; init; }
    public double TargetQuantity { get; init; }
    public double ProducedQuantity { get; init; }
    public double CompletionPercent { get; init; }
    public double ProducedRejectQuantity { get; init; }
    public double TargetQuantityPerMinute { get; init; }
    public double QuantityPerMinute { get; init; }
    public DateTime ScheduledToStartAt { get; set; }
    public DateTime ScheduledToCompleteAt { get; init; }
    public DateTime StartedAt { get; set; }
    public DateTime CompletedAt { get; set; }

    public double GetExcessGetProducedQuantity()
    {
        return ProducedQuantity - TargetQuantity;
    }

    public double GetQuantityToRejectQuantityRatio()
    {
        return ProducedQuantity / ProducedRejectQuantity;
    }

    public double GetRejectQuantityPercentage()
    {
        return ProducedRejectQuantity / ProducedQuantity;
    }

    public double GetQuantityPerMinuteDeviation()
    {
        return TargetQuantityPerMinute - QuantityPerMinute;
    }

    public TimeSpan GetScheduledDuration()
    {
        return ScheduledToCompleteAt.Subtract(ScheduledToStartAt);
    }

    public TimeSpan GetDuration()
    {
        return CompletedAt.Subtract(StartedAt);
    }

    public int GetStartDelayInSeconds()
    {
        // Started perfectly on time = +0
        var delay = TimeSpan.Zero;
        var later = false;

        // Started early = -seconds
        if (StartedAt < ScheduledToStartAt)
        {
            delay = ScheduledToStartAt.Subtract(StartedAt);
            later = false;
        }

        // Started late = +seconds
        if (StartedAt > ScheduledToStartAt)
        {
            delay = StartedAt.Subtract(ScheduledToStartAt);
            later = true;
        }

        return later ? +delay.Seconds : -delay.Seconds;
    }

    public int GetCompletionDelayInSeconds()
    {
        // Completed perfectly on time = +0
        var delay = TimeSpan.Zero;
        var later = false;

        // Completed early = -seconds
        if (CompletedAt < ScheduledToCompleteAt)
        {
            delay = ScheduledToCompleteAt.Subtract(CompletedAt);
            later = false;
        }

        // Completed late = +seconds
        if (CompletedAt > ScheduledToCompleteAt)
        {
            delay = CompletedAt.Subtract(ScheduledToCompleteAt);
            later = true;
        }

        return later ? +delay.Seconds : -delay.Seconds;
    }

    public int GetDurationDeviationInSeconds()
    {
        var targetDuration = GetScheduledDuration();
        var duration = GetDuration();

        var slower = targetDuration < duration;
        var deviation = duration - targetDuration;

        return slower ? +deviation.Seconds : -deviation.Seconds;
    }

    // TODO -> Reject
    // TODO -> Material
    // TODO -> Parts
    // TODO -> States -> Downtime and utilization
    // TODO -> Bookings
}