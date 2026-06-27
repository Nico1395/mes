using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Contracts;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Contracts;

internal sealed class ProductionStepEquipmentDto
{
    public required Guid ProductionStepId { get; init; }
    public required Guid EquipmentId { get; init; }
    public EquipmentDto? Equipment { get; init; }
    public required int Quantity { get; set; }
}