namespace Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;

public interface ITerminalRoutine
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}