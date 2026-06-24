using System.Net.Http.Json;
using Mes.Shopfloor.Terminal.Core.Configuration;
using Microsoft.Extensions.Options;

namespace Mes.Shopfloor.Terminal.Core.Domains.ProductionManagement.Subdomains.Resources.Manager;

internal sealed class ProductionUnitModelManager(
    IOptions<ProductionUnitOptions> _options,
    IHttpClientFactory _httpClientFactory) : IProductionUnitModelManager
{
    private ProductionUnitModel? _productionUnitModel;

    public async Task<ProductionUnitModel?> GetCurrentAsync(CancellationToken cancellationToken)
    {
        if (_productionUnitModel != null)
            return _productionUnitModel;

        var key = _options.Value.Key;
        if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("Production unit key not configured.");

        var httpClient = _httpClientFactory.CreateClient("pm");
        _productionUnitModel = await httpClient.GetFromJsonAsync<ProductionUnitModel>($"api/v1/resources/prod-unit/key/{key}?eager=true", cancellationToken);

        return _productionUnitModel;
    }
}