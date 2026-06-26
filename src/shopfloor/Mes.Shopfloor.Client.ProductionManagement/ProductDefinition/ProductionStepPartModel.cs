namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

public record ProductionStepPartModel
{
    public required Guid StepId { get; init; }
    public ProductionStepModel? Step { get; init; }
    public required Guid PartId { get; init; }
    public PartModel? Part { get; init; }
    public required uint Quantity { get; set; }
}