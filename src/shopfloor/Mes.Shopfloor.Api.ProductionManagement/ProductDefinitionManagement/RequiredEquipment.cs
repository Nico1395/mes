namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement;

internal sealed class RequiredEquipment
{
    public required Guid ProductionStepId { get; init; }
    public ProductionStep? ProductionStep { get; init; }
    public required Guid EquipmentId { get; init; }
    public required int Quantity { get; set; }
}