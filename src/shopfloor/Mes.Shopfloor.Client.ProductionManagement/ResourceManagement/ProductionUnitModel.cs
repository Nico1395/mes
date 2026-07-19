namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement;

internal sealed class ProductionUnitModel
{
    public required Guid Id { get; init; }
    public required string Key { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required int TypeId { get; init; }
    public required ProductionUnitTypeModel Type { get; init; }
    public required int GroupId { get; init; }
    public required ProductionUnitGroupModel Group { get; init; }
    public Guid? ProductionLineId { get; set; }
    public Guid? ShopfloorId { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}