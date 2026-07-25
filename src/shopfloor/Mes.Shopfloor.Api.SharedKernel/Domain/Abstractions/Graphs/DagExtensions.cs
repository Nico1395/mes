namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Graphs;

public static class DagExtensions
{
    public static IEnumerable<TDag> Flatten<TDag>(this TDag startNode)
        where TDag : class, IDag<TDag>
    {
        var visited = new HashSet<Guid>();
        var stack = new Stack<TDag>();

        // Concept:
        // (1) Every node is being popped from the stack and marked as visited.
        // (2) That node also gets passed to the action.
        // (3) Then all the node's next nodes are bing pushed to the stack, so each of them is iterated through in (1).
        // (4) If the stack is empty, every node has been iterated over.

        stack.Push(startNode);
        while (stack.Count > 0)
        {
            var node = stack.Pop();
            if (!visited.Add(node.Id))
                continue;

            yield return node;

            for (var i = node.Next.Count - 1; i >= 0; i--)
                stack.Push(node.Next[i]);
        }
    }

    public static IEnumerable<TDag> AsEnumerable<TDag>(this TDag startNode)
        where TDag : class, IDag<TDag>
    {
        return startNode.Flatten();
    }

    public static void Traverse<TDag>(this TDag startNode, Action<TDag> action)
        where TDag : class, IDag<TDag>
    {
        foreach (var node in startNode.Flatten())
            action(node);
    }

    public static bool InsertAfter<TDag>(this TDag startNode, TDag node, Guid fromId)
        where TDag : class, IDag<TDag>
    {
        var fromNode = startNode.Flatten().FirstOrDefault(n => n.Id == fromId);
        if (fromNode == null)
            return false;

        // Check if the node would create a cycle
        if (startNode.IsAfter(node.Id))
            return false;

        // Get all nodes that fromNode currently points to
        var nextNodes = fromNode.Next.ToList();

        // Clear current edges from fromNode (ignore failures if edges don't exist)
        foreach (var nextNode in nextNodes)
        {
            fromNode.Next.Remove(nextNode);
            nextNode.Previous.Remove(fromNode);
            fromNode.RemoveEdge(nextNode.Id);
            nextNode.RemoveEdge(fromNode.Id);
        }

        // Add new node after fromNode
        fromNode.Next.Add(node);
        node.Previous.Add(fromNode);
        if (!fromNode.InsertEdge(node.Id) || !node.InsertEdge(fromNode.Id))
            return false;

        // Connect the new node to the old next nodes
        foreach (var nextNode in nextNodes)
        {
            node.Next.Add(nextNode);
            nextNode.Previous.Add(node);
            if (!node.InsertEdge(nextNode.Id) || !nextNode.InsertEdge(node.Id))
                return false;
        }

        return true;
    }

    public static bool InsertBefore<TDag>(this TDag startNode, TDag node, Guid toId)
        where TDag : class, IDag<TDag>
    {
        var toNode = startNode.Flatten().FirstOrDefault(n => n.Id == toId);
        if (toNode == null)
            return false;

        // Check if the node would create a cycle
        if (startNode.IsBefore(node.Id))
            return false;

        // Get all nodes that currently point to toNode
        var previousNodes = toNode.Previous.ToList();

        // Clear current edges to toNode
        foreach (var prevNode in previousNodes)
        {
            prevNode.Next.Remove(toNode);
            toNode.Previous.Remove(prevNode);
            
            if (!prevNode.RemoveEdge(toNode.Id) || !toNode.RemoveEdge(prevNode.Id))
                return false;
        }

        // Add new node before toNode
        toNode.Previous.Add(node);
        node.Next.Add(toNode);
        
        if (!toNode.InsertEdge(node.Id) || !node.InsertEdge(toNode.Id))
            return false;

        // Connect the old previous nodes to the new node
        foreach (var prevNode in previousNodes)
        {
            prevNode.Next.Add(node);
            node.Previous.Add(prevNode);
            
            if (!prevNode.InsertEdge(node.Id) || !node.InsertEdge(prevNode.Id))
                return false;
        }

        return true;
    }

    public static bool Replace<TDag>(this TDag startNode, TDag node, Guid targetId)
        where TDag : class, IDag<TDag>
    {
        var targetNode = startNode.Flatten().FirstOrDefault(n => n.Id == targetId);
        if (targetNode == null)
            return false;

        // Check if the node would create a cycle
        if (startNode.IsAfter(node.Id) || startNode.IsBefore(node.Id))
            return false;

        // Transfer all previous nodes
        foreach (var prevNode in targetNode.Previous.ToList())
        {
            prevNode.Next.Remove(targetNode);
            prevNode.Next.Add(node);
            node.Previous.Add(prevNode);
            
            if (!prevNode.RemoveEdge(targetNode.Id) || !targetNode.RemoveEdge(prevNode.Id))
                return false;
            
            if (!prevNode.InsertEdge(node.Id) || !node.InsertEdge(prevNode.Id))
                return false;
        }

        // Transfer all next nodes
        foreach (var nextNode in targetNode.Next.ToList())
        {
            nextNode.Previous.Remove(targetNode);
            nextNode.Previous.Add(node);
            node.Next.Add(nextNode);
            
            if (!nextNode.RemoveEdge(targetNode.Id) || !targetNode.RemoveEdge(nextNode.Id))
                return false;
            
            if (!nextNode.InsertEdge(node.Id) || !node.InsertEdge(nextNode.Id))
                return false;
        }

        // Clear target node's references
        targetNode.Next.Clear();
        targetNode.Previous.Clear();

        return true;
    }

    public static bool Remove<TDag>(this TDag startNode, Guid id)
        where TDag : class, IDag<TDag>
    {
        var nodeToRemove = startNode.Flatten().FirstOrDefault(n => n.Id == id);
        return nodeToRemove != null && startNode.Remove(nodeToRemove);
    }

    public static bool Remove<TDag>(this TDag startNode, TDag node)
        where TDag : class, IDag<TDag>
    {
        // Cannot remove the start node if it would break the graph
        if (node.IsStartNode() && node.Next.Count != 1)
            return false;

        // Get previous and next nodes
        var previousNodes = node.Previous.ToList();
        var nextNodes = node.Next.ToList();

        // Connect all previous nodes to all next nodes
        foreach (var prevNode in previousNodes)
        {
            foreach (var nextNode in nextNodes)
            {
                if (!prevNode.Next.Contains(nextNode))
                {
                    prevNode.Next.Add(nextNode);
                    nextNode.Previous.Add(prevNode);
                    
                    if (!prevNode.InsertEdge(nextNode.Id) || !nextNode.InsertEdge(prevNode.Id))
                        return false;
                }
            }

            prevNode.Next.Remove(node);
            
            if (!prevNode.RemoveEdge(node.Id) || !node.RemoveEdge(prevNode.Id))
                return false;
        }

        // Disconnect all next nodes from the removed node
        foreach (var nextNode in nextNodes)
        {
            nextNode.Previous.Remove(node);
            
            if (!nextNode.RemoveEdge(node.Id) || !node.RemoveEdge(nextNode.Id))
                return false;
        }

        // Clear the node's references
        node.Next.Clear();
        node.Previous.Clear();

        return true;
    }

    public static bool IsAfter<TDag>(this TDag node, Guid id)
        where TDag : class, IDag<TDag>
    {
        return node.Flatten().Skip(1).Any(n => n.Id == id);
    }

    public static bool IsBefore<TDag>(this TDag node, Guid id)
        where TDag : class, IDag<TDag>
    {
        var visited = new HashSet<Guid>();

        bool Search(TDag current)
        {
            if (!visited.Add(current.Id))
                return false;

            foreach (var previous in current.Previous)
            {
                if (previous.Id == id)
                    return true;

                if (Search(previous))
                    return true;
            }

            return false;
        }

        return Search(node);
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

        // Clear all assignments so a double call to this method doesn't cause duplicates in the nodes themselves.
        foreach (var node in nodes.Values)
        {
            node.Next.Clear();
            node.Previous.Clear();
        }

        // Basically just flattens all edges and tries to find and assign the target nodes to each other.
        foreach (var edge in nodes.SelectMany(node => node.Value.GetEdges()))
        {
            // Not being able to assign a node correctly is a reason to return null and 'fail' the method.
            if (!nodes.TryGetValue(edge.FromId, out var from) || !nodes.TryGetValue(edge.ToId, out var to))
                return null;

            if (!from.Next.Contains(to))
                from.Next.Add(to);

            if (!to.Previous.Contains(from))
                to.Previous.Add(from);
        }

        // If there is more than one start node, we are cooked.
        var startNodes = nodes.Where(x => x.Value.IsStartNode()).ToList();
        if (startNodes.Count != 1)
            return null;

        // Check whether there is a cycle for validation purposes. This is slower, but the graphs in our application
        // should not be large enough for this to be a serious bottleneck. But if it is one day, then this might be
        // a way for optimizing, given that no cyclic dependencies are stored.
        var startNode = startNodes[0].Value;
        return startNode.HasCycle() ? null : startNode;
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