namespace Mes.Shopfloor.Client.ProductionManagement.ProductionScheduling;

internal sealed class ProductionOrderModel
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public ProductionOrderProgressModel? Progress { get; init; }
    public ProductionOrderPriority Priority { get; init; }
    public ProductionOrderState State { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}