namespace Mes.Shopfloor.Terminal.Core.Infrastructure;

public interface IInitializer
{
    Task InitializeAsync(CancellationToken cancellationToken);
}