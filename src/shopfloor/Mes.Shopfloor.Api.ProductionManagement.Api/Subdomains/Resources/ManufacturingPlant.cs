namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources;

internal sealed class ManufacturingPlant
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<Shopfloor>? Shopfloors { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}