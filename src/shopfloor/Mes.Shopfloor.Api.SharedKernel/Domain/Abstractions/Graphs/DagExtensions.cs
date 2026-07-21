namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Graphs;

public static class DagExtensions
{
    public static void Traverse<TDag>(this TDag startNode, Action<TDag> action)
        where TDag : class, IDag<TDag>
    {
    }

    public static bool ForeachOnPathTo<TDag>(this TDag startNode, Guid id, Action<TDag> action)
        where TDag : class, IDag<TDag>
    {
    }

    public static bool AddNode<TDag>(this TDag startNode, Guid fromId, Guid toId, Action<TDag> action)
        where TDag : class, IDag<TDag>
    {
    }

    public static bool RemoveNode<TDag>(this TDag startNode, Guid id, Action<TDag> action)
        where TDag : class, IDag<TDag>
    {
    }

    public static bool ReplaceNode<TDag>(this TDag startNode, Guid id, Action<TDag> action)
        where TDag : class, IDag<TDag>
    {
    }

    public static bool IsNext<TDag>(this TDag node, Guid id)
        where TDag : class, IDag<TDag>
    {
        return node.Next.Any(n => n.Id == id);
    }

    public static bool IsPrevious<TDag>(this TDag node, Guid id)
        where TDag : class, IDag<TDag>
    {
        return node.Previous.Any(n => n.Id == id);
    }

    public static bool IsAfter<TDag>(this TDag node, Guid id)
        where TDag : class, IDag<TDag>
    {
    }

    public static bool IsBefore<TDag>(this TDag node, Guid id)
        where TDag : class, IDag<TDag>
    {
    }

    public static bool IsStartNode<TDag>(this TDag node)
        where TDag : class, IDag<TDag>
    {
        return node.Previous.Count == 0;
    }

    public static bool IsEndNode<TDag>(this TDag node)
        where TDag : class, IDag<TDag>
    {
        return node.Next.Count == 0;
    }

    public static TDag? ToGraph<TDag>(this Dictionary<Guid, TDag> nodes)
        where TDag : class, IDag<TDag>
    {
        if (nodes.Count == 0)
            return null;

        foreach (var edge in nodes.SelectMany(node => node.Value.GetEdges()))
        {
            if (!nodes.TryGetValue(edge.FromId, out var from))
                return null;

            if (!nodes.TryGetValue(edge.ToId, out var to))
                return null;

            from.Next.Add(to);
            to.Previous.Add(from);
        }

        var startNodes = nodes.Where(x => x.Value.IsStartNode()).ToList();
        if (startNodes.Count != 1)
            return null;

        var startNode = startNodes[0].Value;
        return startNode.HasCycle()
            ? null
            : startNode;
    }

    public static bool HasCycle<TDag>(this TDag start)
        where TDag : class, IDag<TDag>
    {
        var visiting = new HashSet<Guid>();
        var visited = new HashSet<Guid>();

        return Visit(start);

        bool Visit(TDag node)
        {
            if (visiting.Contains(node.Id))
                return true;

            if (visited.Contains(node.Id))
                return false;

            visiting.Add(node.Id);

            if (node.Next.Any(Visit))
            {
                return true;
            }

            visiting.Remove(node.Id);
            visited.Add(node.Id);

            return false;
        }
    }
}