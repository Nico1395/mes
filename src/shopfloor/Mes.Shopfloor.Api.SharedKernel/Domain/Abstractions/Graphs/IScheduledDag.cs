namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Graphs;

public interface IScheduledDag<TDag> : IDag<TDag>
    where TDag : class, IScheduledDag<TDag>
{
    // TODO -> Priority?
    // TODO -> Edges sollten eine Abklingzeit haben.
    // TODO -> Sollten scheduled DAG eventuell eine ArticleId haben oder ist das erst etwas für einen ScheduledOrder.
    
    DateTime StartingAt { get; }
    DateTime EndingAt { get; }
}