namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;

public interface ITerminalRoutine
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}