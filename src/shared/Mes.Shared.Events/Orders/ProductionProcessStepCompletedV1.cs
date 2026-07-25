using Mes.Libraries.RabbitMQ;

namespace Mes.Shared.Events.Orders;

public sealed class ProductionProcessStepCompletedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid ProductionProcessStepId { get; init; }
}