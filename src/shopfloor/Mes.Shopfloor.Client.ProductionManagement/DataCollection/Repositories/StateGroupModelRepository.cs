using System.Net.Http.Json;
using Mes.Shopfloor.Client.SharedKernel.Http;

namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;

internal sealed class StateGroupModelRepository(IHttpClientFactory _httpClientFactory) : IStateGroupModelRepository
{
    public Task<StateGroupModel?> GetByIdAsync(Guid stateGroupId, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateApiClient()
            .GetFromJsonAsync<StateGroupModel>($"/api/v1/pm/data-collection/state-groups/{stateGroupId}", cancellationToken);
    }
}