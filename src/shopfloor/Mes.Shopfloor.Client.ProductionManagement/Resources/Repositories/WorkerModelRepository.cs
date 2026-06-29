using System.Net.Http.Json;

namespace Mes.Shopfloor.Client.ProductionManagement.Resources.Repositories;

internal sealed class WorkerModelRepository(IHttpClientFactory _httpClientFactory) : IWorkerModelRepository
{
    public Task<WorkerModel?> GetByNumberAsync(string workerNumber, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateClient("pm")
            .GetFromJsonAsync<WorkerModel>($"api/v1/resources/worker/Number/{workerNumber}?eager=true", cancellationToken);
    }
}