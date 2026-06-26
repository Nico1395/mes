namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

internal sealed class ProductModel
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ManufacturingProcessId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}