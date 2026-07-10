namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Requests.Http;

internal sealed class ProductHc
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ManufacturingProcessId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}