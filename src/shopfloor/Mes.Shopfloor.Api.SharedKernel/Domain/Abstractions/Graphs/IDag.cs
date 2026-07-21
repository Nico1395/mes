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
    /// <returns>The edges of the node.</returns>
    List<IDagEdge> GetEdges();
}