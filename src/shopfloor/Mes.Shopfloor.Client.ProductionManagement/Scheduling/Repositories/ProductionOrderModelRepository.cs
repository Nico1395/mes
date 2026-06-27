using System.Net.Http.Json;

namespace Mes.Shopfloor.Client.ProductionManagement.Scheduling.Repositories;

internal sealed class ProductionOrderModelRepository(IHttpClientFactory _httpClientFactory) : IProductionOrderModelRepository
{
    public Task<ProductionOrderModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateClient("pm")
            .GetFromJsonAsync<ProductionOrderModel>($"/api/v1/scheduling/prod-order/{id}?eager=true", cancellationToken);
    }
}