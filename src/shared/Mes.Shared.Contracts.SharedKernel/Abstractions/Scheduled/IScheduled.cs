namespace Mes.Shared.Contracts.SharedKernel.Abstractions.Scheduled;

public interface IScheduled
{
    DateTime StartingAt { get; }
    DateTime CompletingAt { get; }
}

public interface IScheduled<TScheduled> : IScheduled
    where TScheduled : class, IScheduled<TScheduled>
{
    Guid Id { get; }
    Guid? ParentId { get; }
    List<TScheduled> Children { get; }
}