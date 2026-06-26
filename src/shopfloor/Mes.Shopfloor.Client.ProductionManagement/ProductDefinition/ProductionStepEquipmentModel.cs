namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

public class ProductionStepEquipmentModel
{
    public required Guid StepId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required int Quantity { get; set; }
}