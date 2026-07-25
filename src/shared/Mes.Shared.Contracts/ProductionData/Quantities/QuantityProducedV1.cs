using Mes.Libraries.RabbitMQ;

namespace Mes.Shared.Contracts.ProductionData.Quantities;

public sealed class QuantityProducedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid WorkerId { get; init; }
    public required double ProducedQuantity { get; init; }
}