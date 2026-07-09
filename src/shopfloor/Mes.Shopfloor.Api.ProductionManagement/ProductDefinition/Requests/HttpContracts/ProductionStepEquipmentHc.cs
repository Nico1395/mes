namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Requests.HttpContracts;

internal sealed class ProductionStepEquipmentHc
{
    public required Guid ProductionStepId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required int Quantity { get; set; }
}