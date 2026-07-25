using Mes.Library.RabbitMQ;

namespace Mes.Shared.Contracts.ProductionData.MaterialsAndParts;

public sealed class MaterialConsumedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid WorkerId { get; init; }
    public required double Quantity { get; init; }
    public required Guid MaterialId { get; init; }
}