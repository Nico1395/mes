namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Requests.Http;

internal sealed record ProductionStepMaterialHc
{
    public required Guid ProductionStepId { get; init; }
    public required Guid MaterialId { get; init; }
    public MaterialHc? Material { get; init; }
    public required double Quantity { get; set; }
}