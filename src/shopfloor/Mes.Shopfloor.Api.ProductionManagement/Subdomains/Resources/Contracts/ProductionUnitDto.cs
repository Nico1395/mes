namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Contracts;

internal sealed class ProductionUnitDto
{
    public Guid Id { get; init; }
    public required string Key { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required int TypeId { get; init; }
    public ProductionUnitTypeDto? Type { get; init; }
    public required int GroupId { get; init; }
    public ProductionUnitGroupDto? Group { get; init; }
    public Guid? ProductionLineId { get; set; }
    public Guid? ShopfloorId { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}