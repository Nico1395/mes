namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling;

internal sealed class ProductionUnitTask
{
    public required Guid ProductionScheduleId { get; init; }
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public ProductionOrder? Order { get; init; }
    public required DateTime StartingAt { get; init; }
    public required DateTime CompletingAt { get; init; }
}