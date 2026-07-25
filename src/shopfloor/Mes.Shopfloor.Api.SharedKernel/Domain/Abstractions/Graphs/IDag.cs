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
    ///         the graph's edges, and this method returns all nodes.
    ///     </para>
    /// </remarks>
    /// <returns>The edges of the node.</returns>
    List<IDagEdge> GetEdges();

    /// <summary>
    /// Called when an edge to an adjacent node is to be synchronized.
    /// </summary>
    /// <param name="otherId">ID of the 'to' node.</param>
    /// <returns><see langword="true"/> if the edge was inserted, <see langword="false"/> otherwise.</returns>
    bool InsertEdge(Guid otherId);

    /// <summary>
    /// Called when an edge to an adjacent node is to be removed and edges are to be synchronized.
    /// </summary>
    /// <param name="otherId">ID of the 'to' node.</param>
    /// <returns><see langword="true"/> if the edge was removed, <see langword="false"/> otherwise.</returns>
    bool RemoveEdge(Guid otherId);
}