namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;

public abstract class TerminalRoutineJob : ITerminalRoutineJob
{
    public abstract int Order { get; }
    public virtual TimeSpan? Timeout => null;

    public abstract Task ExecuteAsync(ITerminalRoutineContext context, CancellationToken cancellationToken);

    public virtual void HydrateContext(ITerminalRoutineContext context)
    {
    }
}