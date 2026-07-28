using Mes.Shared.Contracts.SharedKernel.Abstractions;

namespace Mes.Hub.Edge.Synchronization.MasterData.Application;

internal interface IMasterDataProvider
{
    Task<Dictionary<string, IMasterData[]>> GetAsync(
        string requestShopfloorKey,
        string[] masterDataTypes,
        int? page,
        int? pageSize,
        DateTime? requestLastUpdatedAt,
        CancellationToken cancellationToken);
}