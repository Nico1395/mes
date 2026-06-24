using Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DetailedScheduling;

internal sealed class ProductionOrder
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductId { get; init; }
    public Product? Product { get; init; }
    public required double Quantity { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DateTime TargetDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}