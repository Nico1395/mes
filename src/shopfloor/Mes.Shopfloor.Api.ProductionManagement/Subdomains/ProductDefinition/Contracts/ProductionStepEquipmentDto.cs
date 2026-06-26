namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Contracts;

public class ProductionStepEquipmentDto
{
    public required Guid StepId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required int Quantity { get; set; }
}