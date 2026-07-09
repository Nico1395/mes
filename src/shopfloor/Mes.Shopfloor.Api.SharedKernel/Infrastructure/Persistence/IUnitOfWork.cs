namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

public interface IUnitOfWork
{
    TRepository Repository<TRepository>() where TRepository : IRepository;
    Task CommitAsync(CancellationToken cancellationToken);
}