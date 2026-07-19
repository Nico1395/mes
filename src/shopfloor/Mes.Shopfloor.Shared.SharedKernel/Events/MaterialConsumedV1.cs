using Mes.Shopfloor.Shared.SharedKernel.Messaging;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

public sealed class MaterialConsumedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid WorkerId { get; init; }
    public required double Quantity { get; init; }
    public required Guid MaterialId { get; init; }
}