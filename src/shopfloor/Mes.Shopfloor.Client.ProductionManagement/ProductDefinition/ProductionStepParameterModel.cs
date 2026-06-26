namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

public class ProductionStepParameterModel
{
    public required Guid StepId { get; init; }
    public required string Key { get; set; }
    public required string Value { get; set; }
    public required ProductionStepParameterType Type { get; set; }
}