using Mes.Shopfloor.Shared.ValueObjects;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.ProductDefinition.Contracts;

internal sealed class ProductionStepDto
{
    public Guid Id { get; init; }
    public required Guid ProductionProcessId { get; init; }
    public required int Index { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Duration? Duration { get; set; }
    public Guid? ProductionUnitGroupId { get; set; }
    public List<ProductionStepPartDto>? Parts { get; set; }
    public List<ProductionStepMaterialDto>? Material { get; set; }
    public List<ProductionStepParameterDto>? Parameters { get; set; }
    public List<ProductionStepEquipmentDto>? Equipment { get; set; }
}