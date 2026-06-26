using System.Net.Http.Json;
using Mes.Shopfloor.Client.Configuration;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Client.ProductionManagement.Resources.Repositories;

internal sealed class ProductionUnitModelRepository(IHttpClientFactory _httpClientFactory) : IProductionUnitModelRepository
{
    public Task<ProductionUnitModel?> GetByKeyAsync(string key, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateClient("pm")
            .GetFromJsonAsync<ProductionUnitModel>($"api/v1/resources/prod-unit/key/{key}?eager=true", cancellationToken);
    }
}