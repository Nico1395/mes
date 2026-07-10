namespace Mes.Shopfloor.Api.ProductionManagement.Scheduling;

internal sealed class ProductionUnitSchedule
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionUnitId { get; init; }
    public List<ProductionUnitTask>? Tasks { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}