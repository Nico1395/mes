namespace Mes.Shopfloor.Api.Infrastructure;

public interface IUnitOfWork
{
    TRepository Repository<TRepository>() where TRepository : IRepository;
    Task CommitAsync(CancellationToken cancellationToken);
}