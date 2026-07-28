using Mes.Library.RabbitMQ;

namespace Mes.Shared.Contracts.SharedKernel.ProductionData.Events.Quantities;

public sealed class RejectQuantityProducedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid WorkerId { get; init; }
    public required double ProducedRejectQuantity { get; init; }
    public Guid? RejectId { get; init; }
}