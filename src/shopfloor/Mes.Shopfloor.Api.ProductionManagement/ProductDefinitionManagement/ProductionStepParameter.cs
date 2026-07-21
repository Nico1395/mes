namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement;

internal sealed class ProductionStepParameter
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionStepId { get; init; }
    public required string Key { get; set; }
    public required string Value { get; set; }
    public required ProductionStepParameterType Type { get; set; }
}