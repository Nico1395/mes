namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Contracts;

public record ProductionStepMaterialDto
{
    public required Guid StepId { get; init; }
    public required Guid MaterialId { get; init; }
    public MaterialDto? Material { get; init; }
    public required double Quantity { get; set; }
}