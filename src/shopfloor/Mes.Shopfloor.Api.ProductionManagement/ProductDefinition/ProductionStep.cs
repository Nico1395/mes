using Mes.Shopfloor.Shared.SharedKernel.ValueObjects;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition;

internal sealed class ProductionStep
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionProcessId { get; init; }
    public required int Index { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Duration Duration { get; set; } = Duration.Empty;
    public Guid? ProductionUnitGroupId { get; set; }
    public List<ProductionStepPart>? Parts { get; set; }
    public List<ProductionStepMaterial>? Material { get; set; }
    public List<ProductionStepParameter>? Parameters { get; set; }
    public List<ProductionStepEquipment>? Equipment { get; set; }
}