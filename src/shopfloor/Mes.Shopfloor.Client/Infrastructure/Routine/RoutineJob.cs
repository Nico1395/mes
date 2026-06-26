namespace Mes.Shopfloor.Client.Infrastructure.Routine;

public abstract class RoutineJob : IRoutineJob
{
    public abstract int Order { get; }
    public virtual TimeSpan? Timeout => null;

    public abstract Task ExecuteAsync(IRoutineContext context, CancellationToken cancellationToken);

    public virtual void Synchronize(IRoutineContext context)
    {
    }
}