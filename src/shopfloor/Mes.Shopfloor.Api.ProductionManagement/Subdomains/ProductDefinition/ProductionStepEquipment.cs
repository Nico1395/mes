using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition;

internal sealed class ProductionStepEquipment
{
    public required Guid ProductionStepId { get; init; }
    public ProductionStep? ProductionStep { get; init; }
    public required Guid EquipmentId { get; init; }
    public Equipment? Equipment { get; init; }
    public required int Quantity { get; set; }
}