using Mes.Libraries.RabbitMQ;

namespace Mes.Shared.Events.ProductionUnits;

public sealed class ProductionUnitWentOnlineV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid WorkerId { get; init; }
}