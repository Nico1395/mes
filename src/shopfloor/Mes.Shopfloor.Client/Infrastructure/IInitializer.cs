namespace Mes.Shopfloor.Client.Infrastructure;

public interface IInitializer
{
    Task InitializeAsync(CancellationToken cancellationToken);
}