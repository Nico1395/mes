using Mes.Shared.Contracts.SharedKernel.Abstractions.Graphs;
using Mes.Shopfloor.Api.SharedKernel.Extensions;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledOrder : IScheduledDag<ScheduledOrder>
{
    public required Guid OrderId { get; init; }
    public required Order? Order { get; init; }
    public Guid? ProductionUnitId { get; init; }
    public ScheduledOrderType Type { get; init; } = ScheduledOrderType.Individual;
    public List<ScheduledOrderEdge> Edges { get; init; } = [];
    public List<ScheduledOrderParameter>? Parameters { get; init; }
    public List<ScheduledOrderParts>? Parts { get; init; }
    public List<ScheduledOrderMaterial>? Material { get; init; }
    public List<ScheduledOrderEquipment>? Equipment { get; init; }
    public DateTime ScheduledAt { get; init; } = DateTime.UtcNow;
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<ScheduledOrder> Previous { get; init; } = [];
    public List<ScheduledOrder> Next { get; init; } = [];
    public required DateTime StartingAt { get; init; }
    public required DateTime EndingAt { get; init; }

    public List<IDagEdge> GetEdges()
    {
        return Edges.CastToList<IDagEdge>();
    }

    public bool InsertEdge(Guid otherId)
    {
        var edge = new ScheduledOrderEdge { FromId = Id, ToId = otherId };
        if (Edges.Contains(edge))
            return false;

        Edges.Add(edge);
        return true;
    }

    public bool RemoveEdge(Guid otherId)
    {
        var edge = new ScheduledOrderEdge { FromId = Id, ToId = otherId };
        return Edges.Remove(edge);
    }
}