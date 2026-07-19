namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinitionManagement;

internal sealed record ProductionStepMaterialModel
{
    public required Guid ProductionStepId { get; init; }
    public ProductionStepModel? ProductionStep { get; init; }
    public required Guid MaterialId { get; init; }
    public MaterialModel? Material { get; init; }
    public required double Quantity { get; set; }
}