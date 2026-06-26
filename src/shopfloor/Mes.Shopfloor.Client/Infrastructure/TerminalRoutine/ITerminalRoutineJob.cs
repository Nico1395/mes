namespace Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;

public interface ITerminalRoutineJob
{
    int Order { get; }
    TimeSpan? Timeout { get; }
    Task ExecuteAsync(ITerminalRoutineContext context, CancellationToken cancellationToken);
    void Synchronize(ITerminalRoutineContext context);
}