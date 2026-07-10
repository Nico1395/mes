namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement;

internal sealed class ProductionUnitGroupModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required Guid StateGroupId { get; init; }
    public required Guid RejectGroupId { get; init; }
    public List<ProductionUnitModel>? ProductionUnits { get; init; }
    public List<ProductionUnitGroupQualificationModel>? RequiredQualifications { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}