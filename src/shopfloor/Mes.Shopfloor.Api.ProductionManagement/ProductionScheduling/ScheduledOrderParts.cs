namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledOrderParts
{
    public required Guid ScheduledOrderId { get; init; }
    public required Guid PartId { get; init; }
    public required double Quantity { get; init; }
}