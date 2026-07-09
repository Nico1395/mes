namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Requests.HttpContracts;

internal sealed record ProductionStepPartHc
{
    public required Guid ProductionStepId { get; init; }
    public required Guid PartId { get; init; }
    public PartHc? Part { get; init; }
    public required uint Quantity { get; set; }
}