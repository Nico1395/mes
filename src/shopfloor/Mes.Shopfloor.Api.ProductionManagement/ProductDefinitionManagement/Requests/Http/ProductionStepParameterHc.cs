namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Requests.Http;

internal sealed class ProductionStepParameterHc
{
    public required Guid ProductionStepId { get; init; }
    public required string Key { get; set; }
    public required string Value { get; set; }
    public required int Type { get; set; }
}