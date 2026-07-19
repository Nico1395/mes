namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Requests.Http;

internal sealed class ManufacturingPlantDto
{
    public int Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ShopfloorDto>? Shopfloors { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}