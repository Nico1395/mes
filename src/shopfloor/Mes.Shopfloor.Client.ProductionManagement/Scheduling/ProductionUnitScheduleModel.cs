namespace Mes.Shopfloor.Client.ProductionManagement.Scheduling;

internal sealed class ProductionUnitScheduleModel
{
    public Guid Id { get; init; }
    public required Guid ProductionUnitId { get; init; }
    public List<ProductionUnitTaskModel>? Tasks { get; init; }

    public ProductionUnitTaskModel? GetCurrentTask()
    {
        if (Tasks == null)
            return null;

        var now = DateTime.UtcNow;
        return Tasks.FirstOrDefault(t => t.StartingAt <= now && t.CompletingAt >= now);
    }
}
