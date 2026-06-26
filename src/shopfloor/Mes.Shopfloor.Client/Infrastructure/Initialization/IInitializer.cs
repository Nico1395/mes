namespace Mes.Shopfloor.Client.Infrastructure.Initialization;

public interface IInitializer
{
    Task<IReadOnlyList<InitializationIssue>> InitializeAsync(CancellationToken cancellationToken);
}