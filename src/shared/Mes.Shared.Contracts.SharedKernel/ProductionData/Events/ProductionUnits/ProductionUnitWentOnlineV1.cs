using Mes.Library.RabbitMQ;

namespace Mes.Shared.Contracts.SharedKernel.ProductionData.Events.ProductionUnits;

public sealed class ProductionUnitWentOnlineV1 : Message
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid WorkerId { get; init; }
}