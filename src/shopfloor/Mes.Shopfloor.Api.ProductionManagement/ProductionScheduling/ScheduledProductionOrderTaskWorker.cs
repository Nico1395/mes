namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledProductionOrderTaskWorker
{
    public required Guid ScheduledTaskId { get; init; }
    public required Guid WorkerId { get; init; }
}