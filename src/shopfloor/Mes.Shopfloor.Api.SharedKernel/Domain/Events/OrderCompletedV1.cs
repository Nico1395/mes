using DandyMediator;

namespace Mes.Shopfloor.Api.SharedKernel.Domain.Events;

public sealed record OrderCompletedV1(
    Guid ProductionOrderId,
    DateTime ScheduledToStartAt,
    DateTime ScheduledToCompleteAt,
    DateTime StartedAt,
    DateTime CompletedAt,
    double TargetQuantity,
    double ProducedQuantity,
    double ProducedRejectQuantity) : INotification;