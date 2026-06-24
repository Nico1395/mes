namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition;

public record ProductionStepPart
{
    public required Guid StepId { get; init; }
    public ProductionStep? Step { get; init; }
    public required Guid PartId { get; init; }
    public Part? Part { get; init; }
    public required uint Quantity { get; set; }
}