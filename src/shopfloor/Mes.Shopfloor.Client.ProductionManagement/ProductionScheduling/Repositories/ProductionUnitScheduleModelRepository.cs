using System.Net.Http.Json;
using Mes.Shopfloor.Client.SharedKernel.Http;

namespace Mes.Shopfloor.Client.ProductionManagement.ProductionScheduling.Repositories;

internal sealed class ProductionUnitScheduleModelRepository(IHttpClientFactory _httpClientFactory) : IProductionUnitScheduleModelRepository
{
    public Task<ProductionUnitScheduleModel?> GetByProductionUnitIdAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateApiClient()
            .GetFromJsonAsync<ProductionUnitScheduleModel>($"/api/v1/pm/scheduling/production-unit-schedules/{productionUnitId}", cancellationToken);
    }
}