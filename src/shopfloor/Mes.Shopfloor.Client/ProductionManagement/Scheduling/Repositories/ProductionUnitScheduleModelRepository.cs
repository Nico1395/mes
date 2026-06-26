using System.Net.Http.Json;

namespace Mes.Shopfloor.Client.ProductionManagement.Scheduling.Repositories;

internal sealed class ProductionUnitScheduleModelRepository(IHttpClientFactory _httpClientFactory) : IProductionUnitScheduleModelRepository
{
    public Task<ProductionUnitScheduleModel?> GetByProductionUnitIdAsync(Guid productionUnitId, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateClient("pm")
            .GetFromJsonAsync<ProductionUnitScheduleModel>($"/api/v1/scheduling/prod-unit-schedules/{productionUnitId}", cancellationToken);
    }
}