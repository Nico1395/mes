using Mes.Shopfloor.Api.Infrastructure;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Repositories;

internal interface IWorkerRepository : IRepository
{
    Task<Worker?> GetByNumberAsync(string number, CancellationToken cancellationToken);
    Task<Worker?> GetByNumberEagerAsync(string number, CancellationToken cancellationToken);
}