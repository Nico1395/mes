namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.ProductDefinition.Contracts;

internal sealed record ProductionStepMaterialDto
{
    public required Guid ProductionStepId { get; init; }
    public required Guid MaterialId { get; init; }
    public MaterialDto? Material { get; init; }
    public required double Quantity { get; set; }
}