using System.Net.Http.Json;
using Mes.Shopfloor.Client.SharedKernel.Http;

namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;

internal sealed class RejectGroupModelRepository(IHttpClientFactory _httpClientFactory) : IRejectGroupModelRepository
{
    public Task<RejectGroupModel?> GetByIdAsync(Guid rejectGroupId, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateApiClient()
            .GetFromJsonAsync<RejectGroupModel>($"/api/v1/pm/data-collection/reject-groups/{rejectGroupId}", cancellationToken);
    }
}