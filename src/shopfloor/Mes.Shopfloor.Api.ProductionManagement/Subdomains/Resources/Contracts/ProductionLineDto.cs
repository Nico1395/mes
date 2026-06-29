namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Contracts;

internal sealed class ProductionLineDto
{
    public Guid Id { get; init; }
    public required Guid ShopfloorId { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ProductionUnitDto>? ProductionUnits { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
}