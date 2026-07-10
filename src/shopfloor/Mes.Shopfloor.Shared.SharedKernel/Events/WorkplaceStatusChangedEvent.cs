using Mes.Shopfloor.Shared.SharedKernel.Messaging;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

[MessageRoute("status.changed")]
public sealed class WorkplaceStatusChangedEvent : Message
{
    public required Guid WorkplaceId { get; init; }
    public required Guid OldStatusId { get; init; }
    public required Guid NewStatusId { get; init; }
    public required Guid EmployeeId { get; init; }
}