using Mes.Libraries.RabbitMQ;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

public sealed class OrderScheduledV1 : Message
{
    public required Guid ProductionOrderId { get; init; }
    public required Guid ScheduledProductionOrderId { get; init; }
}