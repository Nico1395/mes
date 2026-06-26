namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

public record ProductionStepMaterialModel
{
    public required Guid StepId { get; init; }
    public ProductionStepModel? Step { get; init; }
    public required Guid MaterialId { get; init; }
    public MaterialModel? Material { get; init; }
    public required double Quantity { get; set; }
}