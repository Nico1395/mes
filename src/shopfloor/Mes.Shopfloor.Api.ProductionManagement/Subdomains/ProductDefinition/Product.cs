namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition;

internal sealed class Product
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ProductionProcessId { get; set; }
    public ProductionProcess? ProductionProcess { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}