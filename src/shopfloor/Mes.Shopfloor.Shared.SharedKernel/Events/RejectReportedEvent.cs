using Mes.Shopfloor.Shared.SharedKernel.Messaging;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

public sealed class RejectReportedEvent : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required int RejectQuantity { get; init; }
    public Guid? RejectId { get; init; }
}