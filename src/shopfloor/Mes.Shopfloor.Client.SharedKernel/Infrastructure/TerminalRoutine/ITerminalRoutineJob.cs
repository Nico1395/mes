namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;

public interface ITerminalRoutineJob
{
    int Order { get; }
    TimeSpan? Timeout { get; }
    Task ExecuteAsync(ITerminalRoutineContext context, CancellationToken cancellationToken);
    void HydrateContext(ITerminalRoutineContext context);
}