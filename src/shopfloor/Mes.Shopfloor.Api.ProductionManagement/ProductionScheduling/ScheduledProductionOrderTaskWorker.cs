namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledProductionOrderTaskWorker
{
    public required Guid TaskId { get; init; }
    public required Guid WorkerId { get; init; }
}