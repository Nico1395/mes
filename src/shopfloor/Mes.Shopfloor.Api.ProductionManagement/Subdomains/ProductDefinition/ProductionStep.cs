using Mes.Shopfloor.Shared.ValueObjects;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition;

public class ProductionStep
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProcessId { get; init; }
    public required int Index { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Duration? Duration { get; set; }
    public Guid? ProductionUnitId { get; set; }
    public List<ProductionStepPart>? Parts { get; set; }
    public List<ProductionStepMaterial>? Material { get; set; }
    public List<ProductionStepParameter>? Parameters { get; set; }
    public List<ProductionStepEquipment>? Equipment { get; set; }
}