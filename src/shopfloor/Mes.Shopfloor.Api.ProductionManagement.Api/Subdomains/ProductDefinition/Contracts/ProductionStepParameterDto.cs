namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.ProductDefinition.Contracts;

internal sealed class ProductionStepParameterDto
{
    public required Guid ProductionStepId { get; init; }
    public required string Key { get; set; }
    public required string Value { get; set; }
    public required int Type { get; set; }
}