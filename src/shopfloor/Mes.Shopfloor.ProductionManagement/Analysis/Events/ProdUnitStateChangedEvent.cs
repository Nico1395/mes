using Mes.Shopfloor.Core.Messaging;

namespace Mes.Shopfloor.ProductionManagement.Core.Analysis.Events;

public sealed class ProdUnitStateChangedEvent : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid OldStateId { get; init; }
    public required Guid NewStateId { get; init; }
}