namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis;

internal sealed class ProductionOrderPartsConsumption
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionOrderId { get; init; }
    public required Guid ProductionUnitId { get; init; }
    public required Guid PartId { get; init; }
    public required int Quantity { get; init; }
    public required DateTime ReportedAt { get; set; }
}