using Mes.Shared.Contracts.SharedKernel.Abstractions.Graphs;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed record ScheduledOrderEdge : IDagEdge
{
    public required Guid FromId { get; init; }
    public required Guid ToId { get; init; }

    public bool Equals(IDagEdge? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        return FromId.Equals(other.FromId) && ToId.Equals(other.ToId);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FromId, ToId);
    }
}