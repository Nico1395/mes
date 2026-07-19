namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Requests.Http;

internal sealed class ShopfloorDto
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ManufacturingPlantId { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ProductionUnitDto>? ProductionUnits { get; set; }
    public List<ProductionLineDto>? ProductionLines { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}