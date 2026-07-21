namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement;

internal sealed class ProductionProcess
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool HasSteps { get; set; }
    public List<ProductionStep>? Steps { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}