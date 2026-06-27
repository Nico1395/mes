namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Contracts;

internal sealed record ProductionStepPartDto
{
    public required Guid ProductionStepId { get; init; }
    public required Guid PartId { get; init; }
    public PartDto? Part { get; init; }
    public required uint Quantity { get; set; }
}