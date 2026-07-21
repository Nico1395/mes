namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Graphs;

public interface IScheduledDag<TDag> : IDag<TDag>
    where TDag : class, IScheduledDag<TDag>
{
    // TODO -> Priority?
    DateTime StartingAt { get; }
    DateTime EndingAt { get; }
}