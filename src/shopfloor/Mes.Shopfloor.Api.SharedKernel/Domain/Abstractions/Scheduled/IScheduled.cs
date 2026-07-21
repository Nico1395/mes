namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Scheduled;

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