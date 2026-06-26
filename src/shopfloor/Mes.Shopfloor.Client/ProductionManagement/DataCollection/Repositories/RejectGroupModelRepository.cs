using System.Net.Http.Json;

namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Repositories;

internal sealed class RejectGroupModelRepository(IHttpClientFactory _httpClientFactory) : IRejectGroupModelRepository
{
    public Task<RejectGroupModel?> GetByIdAsync(Guid rejectGroupId, CancellationToken cancellationToken)
    {
        return _httpClientFactory
            .CreateClient("pm")
            .GetFromJsonAsync<RejectGroupModel>($"api/v1/data-collection/reject-group/{rejectGroupId}?eager=true", cancellationToken);
    }
}