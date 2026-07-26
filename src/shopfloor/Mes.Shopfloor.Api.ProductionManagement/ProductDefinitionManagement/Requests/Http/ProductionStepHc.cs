using Mes.Shared.Contracts.ValueObjects;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Requests.Http;

internal sealed class ProductionStepHc
{
    public Guid Id { get; init; }
    public required Guid ProductionProcessId { get; init; }
    public required int Index { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Duration? Duration { get; set; }
    public Guid? ProductionUnitGroupId { get; set; }
    public List<ProductionStepPartHc>? Parts { get; set; }
    public List<ProductionStepMaterialHc>? Material { get; set; }
    public List<ProductionStepParameterHc>? Parameters { get; set; }
    public List<ProductionStepEquipmentHc>? Equipment { get; set; }
}