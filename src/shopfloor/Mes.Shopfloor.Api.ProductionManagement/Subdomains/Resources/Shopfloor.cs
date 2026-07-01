namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources;

internal sealed class Shopfloor
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required int ManufacturingPlantId { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ProductionUnit>? ProductionUnits { get; set; }
    public List<ProductionLine>? ProductionLines { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}