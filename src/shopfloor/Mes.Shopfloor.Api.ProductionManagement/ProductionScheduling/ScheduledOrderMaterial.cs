namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledOrderMaterial
{
    public required Guid ScheduledOrderId { get; init; }
    public required Guid MaterialId { get; init; }
    public required double Quantity { get; init; }
}