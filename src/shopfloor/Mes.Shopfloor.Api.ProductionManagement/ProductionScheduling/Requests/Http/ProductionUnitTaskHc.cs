namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Requests.Http;

internal sealed class ProductionUnitTaskHc
{
    public required Guid ProductionScheduleId { get; init; }
    public required Guid ProductionUnitId { get; init; }
    public required Guid ProductionOrderId { get; init; }
    public ProductionOrderHc? Order { get; init; }
    public required DateTime StartingAt { get; init; }
    public required DateTime CompletingAt { get; init; }
}