namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

internal sealed class ProductionProcessModel
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<ProductionStepModel>? Steps { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}