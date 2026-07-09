namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.ProductDefinition.Contracts;

internal sealed class ProductionProcessDto
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ProductionStepDto>? Steps { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}