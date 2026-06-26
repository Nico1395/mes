namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Contracts;

public record ProductionStepPartDto
{
    public required Guid StepId { get; init; }
    public required Guid PartId { get; init; }
    public PartDto? Part { get; init; }
    public required uint Quantity { get; set; }
}