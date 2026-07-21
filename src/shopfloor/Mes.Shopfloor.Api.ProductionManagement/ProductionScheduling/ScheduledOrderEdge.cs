using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Graphs;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed record ScheduledOrderEdge : IDagEdge
{
    public required Guid FromId { get; init; }
    public required Guid ToId { get; init; }
}