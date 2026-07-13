using System.Diagnostics.CodeAnalysis;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis;

internal sealed class ProductionOrderStatus
{
    public required Guid ProductionOrderId { get; init; }
    public required Guid ProductionOrderScheduleId { get; init; }
    public required Guid ProductId { get; init; }
    public int Version { get; set; }
    public Guid? ProductionProcessId { get; init; }
    public Guid? ProductionProcessStepId { get; set; }
    public required ProductionOrderPriority Priority { get; init; }
    public required string Name { get; set; }
    public required double TargetQuantity { get; init; }
    public double ProducedQuantity { get; set; }
    public double ProgressPercent { get; set; }
    public double ProducedRejectQuantity { get; set; }
    public List<ProductionOrderProducedQuantity> ProducedQuantities { get; init; } = [];
    public List<ProductionOrderProducedReject> ProducedRejectQuantities { get; init; } = [];
    public List<ProductionOrderMaterialConsumption> MaterialConsumption { get; init; } = [];
    public List<ProductionOrderPartsConsumption> PartConsumption { get; init; } = [];
    public DateTime? StartedAt { get; set; }       // Represents the time when the order has actually started, not when it was scheduled
    public DateTime? CompletedAt { get; set; }
    public required DateTime ScheduledToStartAt { get; set; }
    public required DateTime ScheduledToCompleteAt { get; init; }
    public ProductionOrderStatusState State { get; set; } = ProductionOrderStatusState.Scheduled;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    [MemberNotNullWhen(true, nameof(StartedAt))]
    public bool HasStarted()
    {
        return StartedAt.HasValue &&
               State > ProductionOrderStatusState.Scheduled;
    }

    [MemberNotNull(nameof(StartedAt))]
    public void StartOrResume()
    {
        StartedAt ??= DateTime.UtcNow;
        State = ProductionOrderStatusState.InProduction;
    }
    
    [MemberNotNullWhen(true, nameof(StartedAt))]
    [MemberNotNullWhen(true, nameof(CompletedAt))]
    public bool IsCompleted()
    {
        return HasStarted() &&
               CompletedAt.HasValue &&
               ProgressPercent >= 100 &&
               State == ProductionOrderStatusState.Completed;
    }

    public void AddProducedQuantity(Guid productionUnitId, double quantity, DateTime reportedAt)
    {
        StartOrResume();

        var producedQuantity = new ProductionOrderProducedQuantity
        {
            ProductionOrderId = ProductionOrderId,
            ProductionUnitId = productionUnitId,
            Quantity = quantity,
            ReportedAt = reportedAt,
        };

        ProducedQuantities.Add(producedQuantity);
        ProducedQuantity += producedQuantity.Quantity;
        ProgressPercent = ProducedQuantity / TargetQuantity * 100;

        if (ProgressPercent >= 100)
        {
            State = ProductionOrderStatusState.Completed;
            CompletedAt = DateTime.UtcNow;
        }

        Touch();
    }

    public double GetTargetQuantityPerMinute()
    {
        if (!StartedAt.HasValue)
            return 0;

        var totalMinutes = (ScheduledToCompleteAt - StartedAt.Value).TotalMinutes;
        return totalMinutes > 0 ? TargetQuantity / totalMinutes : 0;
    }

    public double GetCurrentQuantityPerMinute()
    {
        if (StartedAt == null)
            return 0;

        var totalMinutes = (DateTime.UtcNow - StartedAt.Value).TotalMinutes;
        return totalMinutes > 0 ? ProducedQuantity / totalMinutes : 0;
    }

    public double GetQuantityLeftToBeProduced()
    {
        if (IsCompleted())
            return 0;

        return TargetQuantity - ProducedQuantity;
    }

    public DateTime GetProjectedCompletionDate()
    {
        if (!HasStarted() || IsCompleted())
            return ScheduledToCompleteAt;

        // Current qty/min could return 0, so a check for division by 0 is mandatory, but not sure whether this is the
        // best return value for that case.
        var currentQtyPerMin = GetCurrentQuantityPerMinute();
        if (currentQtyPerMin <= 0)
            return ScheduledToCompleteAt;

        var quantityLeft = GetQuantityLeftToBeProduced();
        var projectedMinutesLeft = quantityLeft / currentQtyPerMin;

        return DateTime.UtcNow.AddMinutes(projectedMinutesLeft);
    }

    internal void Touch() => UpdatedAt = DateTime.UtcNow;
}