namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

internal sealed class ProductionStepParameterModel
{
    public required Guid ProductionStepId { get; init; }
    public required string Key { get; set; }
    public required string Value { get; set; }
    public required ProductionStepParameterType Type { get; set; }
}