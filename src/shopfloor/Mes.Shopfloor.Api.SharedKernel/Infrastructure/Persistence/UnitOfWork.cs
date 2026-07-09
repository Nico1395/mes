using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

public sealed class UnitOfWork(DbContext _context, IServiceProvider _services) : IUnitOfWork
{
    public TRepository Repository<TRepository>()
        where TRepository : IRepository
    {
        return _services.GetRequiredService<TRepository>();
    }

    public Task CommitAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}