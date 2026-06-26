namespace Mes.Shopfloor.Client.Infrastructure.Initialization;

public interface IInitializationJob
{
    int Order { get; }
    Task InitializeAsync(InitializationContext context, CancellationToken cancellationToken);
}