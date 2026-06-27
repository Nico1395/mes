using Mes.Shopfloor.Client.ProductionManagement.Resources;

namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

internal sealed class ProductionStepEquipmentModel
{
    public required Guid StepId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required EquipmentModel Equipment { get; init; }
    public required int Quantity { get; set; }
}