using Mes.Libraries.RabbitMQ;

namespace Mes.Shared.Events.Orders;

public sealed class OrderNotOnTrackV1 : Message
{
    public required Guid ProductionOrderId { get; init; }
    public required double TargetQuantity { get; init; }
    public required double ProducedQuantity { get; init; }
    public required double QuantityLeftToBeProduced { get; init; }
    public required double TargetQuantityPerMinute { get; init; }
    public required double CurrentQuantityPerMinute { get; init; }
    public required double CurrentDeviation { get; init; }
    public required DateTime StartedAt { get; init; }
    public required DateTime ScheduledToCompleteAt { get; init; }
    public required DateTime ProjectedCompletionDate { get; init; }
}