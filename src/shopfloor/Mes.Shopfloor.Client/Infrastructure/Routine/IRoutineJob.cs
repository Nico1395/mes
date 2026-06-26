namespace Mes.Shopfloor.Client.Infrastructure.Routine;

public interface IRoutineJob
{
    int Order { get; }
    TimeSpan? Timeout { get; }
    Task ExecuteAsync(IRoutineContext context, CancellationToken cancellationToken);
    void Synchronize(IRoutineContext context);
}