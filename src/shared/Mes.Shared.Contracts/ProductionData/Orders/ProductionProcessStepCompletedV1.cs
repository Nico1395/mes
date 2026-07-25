using Mes.Library.RabbitMQ;

namespace Mes.Shared.Contracts.ProductionData.Orders;

public sealed class ProductionProcessStepCompletedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid ProductionProcessStepId { get; init; }
}