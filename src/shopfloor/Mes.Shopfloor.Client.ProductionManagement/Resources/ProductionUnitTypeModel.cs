namespace Mes.Shopfloor.Client.ProductionManagement.Resources;

internal sealed class ProductionUnitTypeModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}