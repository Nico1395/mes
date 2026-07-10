namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Requests.Http;

internal sealed class ProductionUnitTypeDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}