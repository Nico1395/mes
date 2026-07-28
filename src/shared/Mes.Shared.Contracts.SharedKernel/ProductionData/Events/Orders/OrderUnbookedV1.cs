using Mes.Library.RabbitMQ;

namespace Mes.Shared.Contracts.SharedKernel.ProductionData.Events.Orders;

public sealed class OrderUnbookedV1 : Message
{
    public required Guid ProductionOrderId { get; init; }
    public required Guid NewProductionOrderId { get; init; }
    public required DateTime UnbookedAt { get; init; }
}