namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Requests.Http;

internal sealed class ProductionProcessHc
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ProductionStepHc>? Steps { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}