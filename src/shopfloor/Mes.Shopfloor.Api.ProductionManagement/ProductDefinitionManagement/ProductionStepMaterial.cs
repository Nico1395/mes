namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement;

internal sealed record ProductionStepMaterial
{
    public required Guid ProductionStepId { get; init; }
    public ProductionStep? ProductionStep { get; init; }
    public required Guid MaterialId { get; init; }
    public Material? Material { get; init; }
    public required double Quantity { get; set; }
}