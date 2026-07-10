using Mes.Shopfloor.Shared.SharedKernel.Messaging;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

public sealed class ProdUnitStateChangedEvent : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid OldStateId { get; init; }
    public required Guid NewStateId { get; init; }
}