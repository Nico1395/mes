namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledOrderParameter
{
    public required Guid ScheduledOrderId { get; init; }
    public required Guid ParameterId { get; init; }
}