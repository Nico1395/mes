namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Requests.Http;

internal sealed class ProductionStepEquipmentHc
{
    public required Guid ProductionStepId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required int Quantity { get; set; }
}