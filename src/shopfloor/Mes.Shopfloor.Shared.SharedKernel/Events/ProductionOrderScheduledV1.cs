using Mes.Shopfloor.Shared.SharedKernel.Messaging;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

public sealed class ProductionOrderScheduledV1 : Message
{
    public required Guid ProductionOrderId { get; init; }
}