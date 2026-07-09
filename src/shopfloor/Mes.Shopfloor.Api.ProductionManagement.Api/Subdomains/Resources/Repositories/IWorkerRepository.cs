using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources.Repositories;

internal interface IWorkerRepository : IRepository
{
    Task<Worker?> GetByNumberAsync(string number, CancellationToken cancellationToken);
    Task<Worker?> GetByNumberEagerAsync(string number, CancellationToken cancellationToken);
}