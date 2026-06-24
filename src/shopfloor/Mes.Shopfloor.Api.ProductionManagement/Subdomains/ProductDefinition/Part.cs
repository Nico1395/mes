namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition;

public class Part
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Sku { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}