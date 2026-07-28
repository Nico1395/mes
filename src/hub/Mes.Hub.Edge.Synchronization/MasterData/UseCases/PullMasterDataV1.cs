using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Hub.Edge.Synchronization.MasterData.Application;
using Mes.Shared.Contracts.SharedKernel.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Mes.Hub.Edge.Synchronization.MasterData.UseCases;

internal static class PullMasterDataV1
{
    public sealed class PullMasterDataV1Dto
    {
        public required string ShopfloorKey { get; init; }
        public required string[] Types { get; init; }
        public int? Page { get; init; }
        public int? PageSize { get; init; }
        public DateTime? LastUpdatedAt { get; init; }
    }

    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/v1/synchronization/master-data/pull", async (
                [FromServices] IMediator mediator,
                [FromBody] PullMasterDataV1Dto dto) =>
            {
                var query = new Query(dto.ShopfloorKey, dto.Types, dto.Page, dto.PageSize, dto.LastUpdatedAt);
                var response = await mediator.SendAsync<Query, Dictionary<string, IMasterData[]>>(query);

                return response.ToResult();
            });
        }
    }

    private sealed record Query(
        string ShopfloorKey,
        string[] Types,
        int? Page,
        int? PageSize,
        DateTime? LastUpdatedAt) : IQuery<Dictionary<string, IMasterData[]>>;

    private sealed class QueryHandler(IMasterDataProvider masterDataProvider) : IQueryHandler<Query, Dictionary<string, IMasterData[]>>
    {
        public async Task<IQueryResponse<Dictionary<string, IMasterData[]>>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var masterData = await masterDataProvider.GetAsync(
                request.ShopfloorKey,
                request.Types,
                request.Page,
                request.PageSize,
                request.LastUpdatedAt,
                cancellationToken);

            return QueryResponse.OK_200(masterData).Build();
        }
    }
}