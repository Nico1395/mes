namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Contracts;

public class ProductionStepParameterDto
{
    public required Guid StepId { get; init; }
    public required string Key { get; set; }
    public required string Value { get; set; }
    public required int Type { get; set; }
}