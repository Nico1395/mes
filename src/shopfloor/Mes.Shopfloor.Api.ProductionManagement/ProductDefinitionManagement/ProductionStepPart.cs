namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement;

internal sealed record ProductionStepPart
{
    public required Guid ProductionStepId { get; init; }
    public ProductionStep? ProductionStep { get; init; }
    public required Guid PartId { get; init; }
    public Part? Part { get; init; }
    public required uint Quantity { get; set; }
}