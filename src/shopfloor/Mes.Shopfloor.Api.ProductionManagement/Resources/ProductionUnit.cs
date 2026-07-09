namespace Mes.Shopfloor.Api.ProductionManagement.Resources;

internal sealed class ProductionUnit
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Key { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required int TypeId { get; set; }
    public ProductionUnitType? Type { get; set; }
    public required int GroupId { get; set; }
    public ProductionUnitGroup? Group { get; set; }
    public Guid? ProductionLineId { get; set; }
    public Guid? ShopfloorId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}