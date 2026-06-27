namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition;

internal sealed class ProductionStepParameter
{
    public required Guid ProductionStepId { get; init; }
    public required string Key { get; set; }
    public required string Value { get; set; }
    public required ProductionStepParameterType Type { get; set; }
}