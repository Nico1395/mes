namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionUnitStatus;

internal sealed class ProductionUnitProducedQuantity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionOrderId { get; init; }
    public required Guid ProductionUnitId { get; init; }
    public required double Quantity { get; init; }
    public required DateTime ReportedAt { get; set; }
}