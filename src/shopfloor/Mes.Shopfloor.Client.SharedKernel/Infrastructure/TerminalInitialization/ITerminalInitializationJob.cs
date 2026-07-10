namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalInitialization;

public interface ITerminalInitializationJob
{
    int Order { get; }
    Task InitializeAsync(TerminalInitializationContext context, CancellationToken cancellationToken);
}