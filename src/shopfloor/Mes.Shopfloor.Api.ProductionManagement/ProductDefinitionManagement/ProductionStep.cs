using Mes.Shared.Contracts.SharedKernel.ValueObjects;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement;

internal sealed class ProductionStep
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionProcessId { get; init; }
    public required int Index { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Duration Duration { get; set; } = Duration.Empty;
    public required Guid ProductionUnitGroupId { get; set; }
    public List<ProductionStepParameter>? Parameters { get; set; }
    public List<RequiredPart>? Parts { get; set; }
    public List<RequiredMaterial>? Material { get; set; }
    public List<RequiredEquipment>? Equipment { get; set; }
}