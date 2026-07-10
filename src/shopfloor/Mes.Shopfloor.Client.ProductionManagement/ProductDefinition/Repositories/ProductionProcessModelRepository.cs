using System.Net.Http.Json;
using Mes.Shopfloor.Client.SharedKernel.Http;

namespace Mes.Shopfloor.Client.ProductionManagement.ProductDefinition.Repositories;

internal sealed class ProductionProcessModelRepository(IHttpClientFactory _httpClientFactory) : IProductionProcessModelRepository
{
    public Task<ProductionProcessModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateApiClient()
            .GetFromJsonAsync<ProductionProcessModel>($"/api/v1/pm/product-definition/production-processes/{id}", cancellationToken);
    }
}