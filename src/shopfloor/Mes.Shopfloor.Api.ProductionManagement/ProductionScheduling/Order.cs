namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class Order
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductId { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DateTime TargetDate { get; init; }
    public OrderPriority Priority { get; init; } = OrderPriority.Lowest;
    public required double TargetQuantity { get; init; }
    public required double AcceptableDeviationPercent { get; init; }
    public bool IsScheduled { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}