namespace Mes.Shopfloor.Client.Infrastructure.Routine;

public interface IRoutine
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}