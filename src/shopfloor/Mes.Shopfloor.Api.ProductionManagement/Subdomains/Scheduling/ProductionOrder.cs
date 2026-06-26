namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling;

internal sealed class ProductionOrder
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductId { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public ProductionOrderProgress? Progress { get; init; }
    public ProductionOrderPriority Priority { get; init; } = ProductionOrderPriority.Lowest;
    public ProductionOrderState State { get; set; } = ProductionOrderState.Defined;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}