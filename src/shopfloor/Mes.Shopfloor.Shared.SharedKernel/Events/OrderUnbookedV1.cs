using Mes.Shopfloor.Shared.SharedKernel.Messaging;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

public sealed class OrderUnbookedV1 : Message
{
    public required Guid ProductionOrderId { get; init; }
    public required Guid NewProductionOrderId { get; init; }
    public required DateTime UnbookedAt { get; init; }
}