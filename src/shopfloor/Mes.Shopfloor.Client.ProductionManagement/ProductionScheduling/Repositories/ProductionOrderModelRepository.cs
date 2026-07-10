using System.Net.Http.Json;
using Mes.Shopfloor.Client.SharedKernel.Http;

namespace Mes.Shopfloor.Client.ProductionManagement.ProductionScheduling.Repositories;

internal sealed class ProductionOrderModelRepository(IHttpClientFactory _httpClientFactory) : IProductionOrderModelRepository
{
    public Task<ProductionOrderModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateApiClient()
            .GetFromJsonAsync<ProductionOrderModel>($"/api/v1/pm/scheduling/production-orders/{id}", cancellationToken);
    }
}