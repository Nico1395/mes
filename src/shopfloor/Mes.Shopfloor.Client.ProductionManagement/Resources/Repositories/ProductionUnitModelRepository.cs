using System.Net.Http.Json;
using Mes.Shopfloor.Client.SharedKernel.Configuration;
using Mes.Shopfloor.Client.SharedKernel.Http;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Client.ProductionManagement.Resources.Repositories;

internal sealed class ProductionUnitModelRepository(IHttpClientFactory _httpClientFactory) : IProductionUnitModelRepository
{
    public Task<ProductionUnitModel?> GetByKeyAsync(string key, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateApiClient()
            .GetFromJsonAsync<ProductionUnitModel>($"/api/v1/pm/resources/production-unit/by-key/{key}", cancellationToken);
    }
}