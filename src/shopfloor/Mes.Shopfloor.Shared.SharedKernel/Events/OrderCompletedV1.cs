using Mes.Libraries.RabbitMQ;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

public sealed class OrderCompletedV1 : Message
{
    public required Guid ProductionOrderId { get; init; }
    public required DateTime ScheduledToStartAt { get; init; }
    public required DateTime ScheduledToCompleteAt { get; init; }
    public required DateTime StartedAt { get; init; }
    public required DateTime CompletedAt { get; init; }
    public required double TargetQuantity { get; init; }
    public required double ProducedQuantity { get; init; }
    public required double ProducedRejectQuantity { get; init; }
}