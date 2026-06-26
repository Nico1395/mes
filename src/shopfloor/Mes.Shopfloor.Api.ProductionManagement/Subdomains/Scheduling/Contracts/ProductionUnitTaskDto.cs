namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Contracts;

internal sealed class ProductionUnitTaskDto
{
    public required Guid ProductionScheduleId { get; init; }
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public ProductionOrderDto? Order { get; init; }
    public required DateTime StartingAt { get; init; }
    public required DateTime CompletingAt { get; init; }
}