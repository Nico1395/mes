namespace Mes.Shared.Contracts.SharedKernel.Abstractions.Graphs;

/// <summary>
/// Abstraction for the edge of a directed acyclic graph.
/// </summary>
public interface IDagEdge : IEquatable<IDagEdge>
{
    /// <summary>
    /// ID of the current node.
    /// </summary>
    Guid FromId { get; }

    /// <summary>
    /// ID of the related node.
    /// </summary>
    Guid ToId { get; }
}