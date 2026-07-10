namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Requests.Http;

internal sealed class ProductionOrderProgressHc
{
    public required Guid ProductionOrderId { get; init; }
    public required double TargetQuantity { get; init; }
    public required double ProducedQuantity { get; init; }
    public required DateTime TargetDate { get; init; }
    public Guid? ProductionProcessId { get; init; }
    public Guid? ProductionProcessStepId { get; init; }
    public DateTime UpdatedAt { get; init; }
}