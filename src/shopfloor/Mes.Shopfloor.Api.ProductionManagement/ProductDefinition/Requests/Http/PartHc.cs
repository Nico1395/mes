namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Requests.Http;

public class PartHc
{
    public Guid Id { get; init; }
    public required string Sku { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}