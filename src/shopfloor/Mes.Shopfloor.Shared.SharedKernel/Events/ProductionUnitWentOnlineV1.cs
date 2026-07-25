using Mes.Libraries.RabbitMQ;

namespace Mes.Shopfloor.Shared.SharedKernel.Events;

public sealed class ProductionUnitWentOnlineV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid WorkerId { get; init; }
}