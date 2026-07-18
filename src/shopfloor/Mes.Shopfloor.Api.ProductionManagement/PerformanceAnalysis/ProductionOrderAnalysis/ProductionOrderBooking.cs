namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis;

internal sealed class ProductionOrderBooking
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid ScheduledTaskId { get; init; }
    public required DateTime BookedAt { get; init; }
}