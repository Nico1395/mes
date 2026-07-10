namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Requests.Http;

internal sealed class ProductionUnitGroupDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required Guid StateGroupId { get; init; }
    public required Guid RejectGroupId { get; init; }
    public List<ProductionUnitDto>? ProductionUnits { get; init; }
    public List<ProductionUnitGroupQualificationDto>? RequiredQualifications { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}