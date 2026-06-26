using Mes.Shopfloor.Shared.ValueObjects;

namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition;

public class ProductionStepModel
{
    public Guid Id { get; init; }
    public required Guid ProcessId { get; init; }
    public required int Index { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Duration? Duration { get; set; }
    public Guid? ProductionUnitId { get; set; }
    public List<ProductionStepPartModel>? Parts { get; set; }
    public List<ProductionStepMaterialModel>? Material { get; set; }
    public List<ProductionStepParameterModel>? Parameters { get; set; }
    public List<ProductionStepEquipmentModel>? Equipment { get; set; }
}