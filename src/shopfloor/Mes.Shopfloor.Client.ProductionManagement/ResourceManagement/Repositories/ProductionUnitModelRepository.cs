using System.Net.Http.Json;
using Mes.Shopfloor.Client.SharedKernel.Http;

namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement.Repositories;

internal sealed class ProductionUnitModelRepository(IHttpClientFactory _httpClientFactory) : IProductionUnitModelRepository
{
    public Task<ProductionUnitModel?> GetByKeyAsync(string key, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateApiClient()
            .GetFromJsonAsync<ProductionUnitModel>($"/api/v1/pm/resources/production-unit/by-key/{key}", cancellationToken);
    }
}