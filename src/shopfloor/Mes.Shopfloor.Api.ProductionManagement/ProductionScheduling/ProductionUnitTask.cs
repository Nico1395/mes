namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ProductionUnitTask
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionUnitScheduleId { get; init; }
    public required Guid ScheduledProductionOrderTaskId { get; init; }
    public ScheduledProductionOrderTask? ScheduledProductionOrderTask { get; init; }
    public required DateTime StartingAt { get; init; }
    public required DateTime CompletingAt { get; init; }
}