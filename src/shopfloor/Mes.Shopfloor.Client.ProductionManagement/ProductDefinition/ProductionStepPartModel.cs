namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

internal sealed record ProductionStepPartModel
{
    public required Guid ProductionStepId { get; init; }
    public ProductionStepModel? ProductionStep { get; init; }
    public required Guid PartId { get; init; }
    public PartModel? Part { get; init; }
    public required uint Quantity { get; set; }
}