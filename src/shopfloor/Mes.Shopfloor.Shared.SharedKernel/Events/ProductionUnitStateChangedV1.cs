using Mes.Shopfloor.Shared.SharedKernel.Messaging;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

[MessageRoute("state.changed")]
public sealed class ProductionUnitStateChangedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid OldStateId { get; init; }
    public required Guid NewStateId { get; init; }
}