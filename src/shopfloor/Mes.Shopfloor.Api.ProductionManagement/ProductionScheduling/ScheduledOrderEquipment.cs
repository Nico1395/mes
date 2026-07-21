namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledOrderEquipment
{
    public required Guid ScheduledOrderId { get; init; }
    public required Guid EquipmentId { get; init; }
    public required double Quantity { get; init; }
}