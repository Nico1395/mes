using Mes.Libraries.RabbitMQ;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

public sealed class OrderBookedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid ScheduledTaskId { get; init; }
    public Guid? PreviousProductionOrderId { get; init; }
}