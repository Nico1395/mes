using Mes.Libraries.RabbitMQ;

namespace Mes.Shared.Contracts.ProductionData.Orders;

public sealed class OrderScheduledV1 : Message
{
    public required Guid ProductionOrderId { get; init; }
    public required Guid ScheduledProductionOrderId { get; init; }
}