using Mes.Library.RabbitMQ;

namespace Mes.Shared.Contracts.SharedKernel.ProductionData.Events.Orders;

public sealed class OrderScheduledV1 : Message
{
    public required Guid ProductionOrderId { get; init; }
    public required Guid ScheduledProductionOrderId { get; init; }
}