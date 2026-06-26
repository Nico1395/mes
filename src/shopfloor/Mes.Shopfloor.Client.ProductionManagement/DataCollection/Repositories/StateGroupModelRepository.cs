using System.Net.Http.Json;

namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;

internal sealed class StateGroupModelRepository(IHttpClientFactory _httpClientFactory) : IStateGroupModelRepository
{
    public Task<StateGroupModel?> GetByIdAsync(Guid stateGroupId, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateClient("pm")
            .GetFromJsonAsync<StateGroupModel>($"api/v1/data-collection/state-group/{stateGroupId}?eager=true", cancellationToken);
    }
}