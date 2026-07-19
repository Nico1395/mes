namespace Mes.Shopfloor.Client.ProductionManagement.ProductionScheduling;

internal sealed class ProductionUnitTaskModel
{
    public required Guid ProductionScheduleId { get; init; }
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public ProductionOrderModel? Order { get; init; }
    public required DateTime StartingAt { get; init; }
    public required DateTime CompletingAt { get; init; }
}