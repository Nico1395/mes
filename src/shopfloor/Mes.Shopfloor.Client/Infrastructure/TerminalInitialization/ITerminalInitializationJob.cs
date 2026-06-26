namespace Mes.Shopfloor.Client.Infrastructure.TerminalInitialization;

public interface ITerminalInitializationJob
{
    int Order { get; }
    Task InitializeAsync(TerminalInitializationContext context, CancellationToken cancellationToken);
}