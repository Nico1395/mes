namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.ProductDefinition.Contracts;

internal sealed record ProductionStepPartDto
{
    public required Guid ProductionStepId { get; init; }
    public required Guid PartId { get; init; }
    public PartDto? Part { get; init; }
    public required uint Quantity { get; set; }
}