namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Graphs;

/// <summary>
/// Abstraction for a directed acyclic graph.
/// </summary>
/// <remarks>
///     <para>
///         For more read <see href="https://en.wikipedia.org/wiki/Directed_acyclic_graph"/>
///     </para>
/// </remarks>
/// <typeparam name="TDag">Type of next and previous nodes.</typeparam>
public interface IDag<TDag>
    where TDag : IDag<TDag>
{
    /// <summary>
    /// ID of the node.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Next nodes.
    /// </summary>
    List<TDag> Next { get; }

    /// <summary>
    /// Previous nodes.
    /// </summary>
    List<TDag> Previous { get; }

    /// <summary>
    /// Gets the edges of the node.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Only returns edges of <b>the calling node</b>. This does <b>not</b> mean that the root node contains all
    ///         the graph's edges and this method returns all nodes.
    ///     </para>
    /// </remarks>
    /// <returns>The edges of the node.</returns>
    internal List<IDagEdge> GetEdges();

    /// <summary>
    /// Called when a node is inserted and edges are to be synchronized.
    /// </summary>
    /// <param name="id">ID of this node.</param>
    /// <param name="toId">ID of the 'to' node.</param>
    internal void InsertEdge(Guid id, Guid toId);

    /// <summary>
    /// Called when a node is removed and edges are to be synchronized.
    /// </summary>
    /// <param name="fromId">ID of the 'from' node.</param>
    /// <param name="toId">ID of the 'to' node.</param>
    internal void RemoveEdge(Guid fromId, Guid toId);
}