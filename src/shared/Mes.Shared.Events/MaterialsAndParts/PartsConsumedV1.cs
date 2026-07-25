using Mes.Libraries.RabbitMQ;

namespace Mes.Shared.Events.MaterialsAndParts;

public sealed class PartsConsumedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid WorkerId { get; init; }
    public required int Quantity { get; init; }
    public required Guid PartId { get; init; }
}