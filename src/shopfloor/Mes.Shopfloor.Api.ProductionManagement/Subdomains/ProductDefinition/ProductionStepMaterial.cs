namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition;

public record ProductionStepMaterial
{
    public required Guid StepId { get; init; }
    public ProductionStep? Step { get; init; }
    public required Guid MaterialId { get; init; }
    public Material? Material { get; init; }
    public required double Quantity { get; set; }
}