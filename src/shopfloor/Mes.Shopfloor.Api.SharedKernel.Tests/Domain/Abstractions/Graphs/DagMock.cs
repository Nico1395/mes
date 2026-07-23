using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Graphs;
using Mes.Shopfloor.Api.SharedKernel.Extensions;

namespace Mes.Shopfloor.Api.SharedKernel.Tests.Domain.Abstractions.Graphs;

public class DagMock : IDag<DagMock>
{
    public Guid Id { get; init;  } = Guid.NewGuid();
    public List<DagMock> Next { get; set; } = [];
    public List<DagMock> Previous { get; set; } = [];
    public List<DagEdgeMock> Edges { get; set; } = [];

    public List<IDagEdge> GetEdges()
    {
        return Edges.CastToList<IDagEdge>();
    }

    public bool InsertEdge(Guid otherId)
    {
        var edge = new DagEdgeMock { FromId = Id, ToId = otherId };
        if (Edges.Contains(edge))
            return false;

        Edges.Add(edge);
        return true;
    }

    public bool RemoveEdge(Guid otherId)
    {
        var edge = new DagEdgeMock { FromId = Id, ToId = otherId };
        return Edges.Remove(edge);
    }
}