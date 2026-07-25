using Mes.Libraries.RabbitMQ;

namespace Mes.Shared.Events.Orders;

public sealed class OrderBookedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid ScheduledTaskId { get; init; }
    public Guid? PreviousProductionOrderId { get; init; }
}