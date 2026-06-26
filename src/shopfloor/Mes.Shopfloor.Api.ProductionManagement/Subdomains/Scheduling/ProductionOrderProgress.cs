namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling;

internal sealed class ProductionOrderProgress
{
    public required Guid ProductionOrderId { get; init; }
    public required double TargetQuantity { get; init; }
    public required double ProducedQuantity { get; init; }
    public required DateTime TargetDate { get; init; }
    public Guid? ProductionProcessId { get; init; }
    public Guid? ProductionProcessStepId { get; init; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}