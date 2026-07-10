using System.Net.Http.Json;
using Mes.Shopfloor.Client.SharedKernel.Http;

namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement.Repositories;

internal sealed class WorkerModelRepository(IHttpClientFactory _httpClientFactory) : IWorkerModelRepository
{
    public Task<WorkerModel?> GetByNumberAsync(string workerNumber, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateApiClient()
            .GetFromJsonAsync<WorkerModel>($"/api/v1/pm/resources/workers/by-number/{workerNumber}", cancellationToken);
    }
}