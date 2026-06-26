using Mes.Shopfloor.Shared.Messaging;

namespace Mes.Shopfloor.Shared.ProductionManagement.Scheduling.Events;

public sealed class ProdUnitStateChangedEvent : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid OldStateId { get; init; }
    public required Guid NewStateId { get; init; }
}