namespace Mes.Shopfloor.Core.Contracts.Events;

public class WorkplaceStatusChangedEvent : EventBase
{
    public required Guid WorkplaceId { get; init; }
    public required Guid OldStatusId { get; init; }
    public required Guid NewStatusId { get; init; }
    public required Guid EmployeeId { get; init; }
}