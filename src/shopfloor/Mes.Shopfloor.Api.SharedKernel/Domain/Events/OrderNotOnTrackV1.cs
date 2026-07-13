using DandyMediator;

namespace Mes.Shopfloor.Api.SharedKernel.Domain.Events;

public sealed record OrderNotOnTrackV1(
    Guid ProductionOrderId,
    double TargetQuantity,
    double ProducedQuantity,
    double QuantityLeftToBeProduced,
    double TargetQuantityPerMinute,
    double CurrentQuantityPerMinute,
    double CurrentDeviation,
    DateTime StartedAt,
    DateTime ScheduledToCompleteAt,
    DateTime ProjectedCompletionDate) : INotification;