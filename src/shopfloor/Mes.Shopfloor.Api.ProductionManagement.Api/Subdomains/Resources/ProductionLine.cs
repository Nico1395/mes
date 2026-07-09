namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources;

internal sealed class ProductionLine
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ShopfloorId { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ProductionUnit>? ProductionUnits { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}