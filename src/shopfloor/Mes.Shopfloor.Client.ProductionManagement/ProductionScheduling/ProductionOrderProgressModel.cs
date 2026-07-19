namespace Mes.Shopfloor.Client.ProductionManagement.ProductionScheduling;

internal sealed class ProductionOrderProgressModel
{
    public Guid ProductionOrderId { get; init; }
    public double TargetQuantity { get; init; }
    public double ProducedQuantity { get; init; }
    public DateTime TargetDate { get; init; }
    public Guid? ProductionProcessId { get; init; }
    public Guid? ProductionProcessStepId { get; init; }
    public DateTime UpdatedAt { get; init; }
}