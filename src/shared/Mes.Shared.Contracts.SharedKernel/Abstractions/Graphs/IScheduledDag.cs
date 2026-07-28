namespace Mes.Shared.Contracts.SharedKernel.Abstractions.Graphs;

public interface IScheduledDag<TDag> : IDag<TDag>
    where TDag : class, IScheduledDag<TDag>
{
    // TODO -> Sollten scheduled DAG eventuell eine ArticleId haben oder ist das erst etwas für einen ScheduledOrder.

    DateTime StartingAt { get; }
    DateTime EndingAt { get; }
}