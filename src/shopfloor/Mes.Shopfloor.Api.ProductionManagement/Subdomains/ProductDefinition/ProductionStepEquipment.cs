namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition;

public class ProductionStepEquipment
{
    public required Guid StepId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required int Quantity { get; set; }
}