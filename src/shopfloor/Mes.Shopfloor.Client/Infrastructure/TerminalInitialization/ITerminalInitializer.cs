namespace Mes.Shopfloor.Client.Infrastructure.TerminalInitialization;

public interface ITerminalInitializer
{
    Task<IReadOnlyList<TerminalInitializationIssue>> InitializeAsync(CancellationToken cancellationToken);
}