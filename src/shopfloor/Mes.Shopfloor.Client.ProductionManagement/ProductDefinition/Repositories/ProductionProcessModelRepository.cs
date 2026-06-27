using System.Net.Http.Json;

namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition.Repositories;

internal sealed class ProductionProcessModelRepository(IHttpClientFactory _httpClientFactory) : IProductionProcessModelRepository
{
    public Task<ProductionProcessModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateClient("pm")
            .GetFromJsonAsync<ProductionProcessModel>($"api/v1/product-definition/prod-process/{id}?eager=true", cancellationToken);
    }
}