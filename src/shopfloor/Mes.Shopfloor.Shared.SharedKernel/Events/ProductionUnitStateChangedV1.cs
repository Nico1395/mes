using Mes.Shopfloor.Shared.SharedKernel.Messaging;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

[MessageRoute("state.changed")]
public sealed class ProductionUnitStateChangedV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public required Guid PreviousStateId { get; init; }
    public required Guid StateId { get; init; }
    public required bool StateIsProductive { get; init; }
    public required bool StateIsIdle { get; init; }
}