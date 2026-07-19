namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalInitialization;

public interface ITerminalInitializer
{
    Task<IReadOnlyList<TerminalInitializationIssue>> InitializeAsync(CancellationToken cancellationToken);
}