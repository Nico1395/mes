namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition;

internal sealed class ProductionStepEquipment
{
    public required Guid ProductionStepId { get; init; }
    public ProductionStep? ProductionStep { get; init; }
    public required Guid EquipmentId { get; init; }
    public required int Quantity { get; set; }
}