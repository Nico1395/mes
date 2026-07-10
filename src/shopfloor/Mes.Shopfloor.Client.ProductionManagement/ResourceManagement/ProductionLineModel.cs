namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement;

internal sealed class ProductionLineModel
{
    public Guid Id { get; init; }
    public required Guid ShopfloorId { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ProductionUnitModel>? ProductionUnits { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
}