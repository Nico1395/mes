using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Graphs;
using Mes.Shopfloor.Api.SharedKernel.Extensions;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledOrder : IScheduledDag<ScheduledOrder>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid OrderId { get; init; }
    public required Order? Order { get; init; }
    public Guid? ProductionUnitId { get; init; }
    public ScheduledOrderType Type { get; init; } = ScheduledOrderType.Individual;
    public List<ScheduledOrderEdge> Edges { get; init; } = [];
    public List<ScheduledOrder> Previous { get; init; } = [];
    public List<ScheduledOrder> Next { get; init; } = [];
    public List<ScheduledOrderParameter>? Parameters { get; init; }
    public List<ScheduledOrderParts>? Parts { get; init; }
    public List<ScheduledOrderMaterial>? Material { get; init; }
    public List<ScheduledOrderEquipment>? Equipment { get; init; }
    public DateTime ScheduledAt { get; init; } = DateTime.UtcNow;
    public required DateTime StartingAt { get; init; }
    public required DateTime EndingAt { get; init; }

    public List<IDagEdge> GetEdges()
    {
        return Edges.CastToList<IDagEdge>();
    }

    public void InsertEdge(Guid id, Guid toId)
    {
        // TODO -> Brauche ich die 'id' überhaupt? Falls nein -> InsertNext() damit die API klarer ist. Allerdings: Was ist wenn das Ding in anderen Verweisen liegt?
        throw new NotImplementedException();
    }

    public void RemoveEdge(Guid fromId, Guid toId)
    {
        // TODO -> Brauche ich 'fromId' überhaupt oder ist das wie oben einfach 'Id'? Falls nein -> RemoveNext() damit die API klarer ist. Allerings: Was ist wenn das Ding in anderen Verweisen liegt?
        throw new NotImplementedException();
    }
}