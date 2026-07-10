namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Requests.Http;

internal sealed record ProductionStepPartHc
{
    public required Guid ProductionStepId { get; init; }
    public required Guid PartId { get; init; }
    public PartHc? Part { get; init; }
    public required uint Quantity { get; set; }
}