// using DandyEndpoints;
// using DandyMediator;
// using DandyMediator.Queries;
// using DandyMediator.Responses;
// using Mes.Shared.Contracts.SharedKernel.Abstractions;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Routing;
//
// namespace Mes.Hub.Edge.SharedKernel.Synchronization.MasterData.UseCases;
//
// internal static class PullMasterDataV1
// {
//     public sealed class PullMasterDataV1Dto
//     {
//         public required string ShopfloorKey { get; init; }
//         public int? Page { get; init; }
//         public int? PageSize { get; init; }
//         public DateTime? LastUpdatedAt { get; init; }
//     }
//
//     private sealed class Endpoint : IEndpoint
//     {
//         // TODO -> Make sure serialization uses GetType() and adds the type name system wide
//
//         public void Map(IEndpointRouteBuilder app)
//         {
//             app.MapPost("/api/v1/synchronization/master-data/pull", async (
//                 [FromServices] IMediator mediator,
//                 [FromBody] PullMasterDataV1Dto dto) =>
//             {
//                 var query = new Query(dto.ShopfloorKey, dto.Page, dto.PageSize, dto.LastUpdatedAt);
//                 var response = await mediator.SendAsync<Query, List<IMasterDataEntity>>(query);
//
//                 return response.ToResult();
//             });
//         }
//     }
//
//     private sealed record Query(
//         string ShopfloorKey,
//         int? Page,
//         int? PageSize,
//         DateTime? LastUpdatedAt) : IQuery<List<IMasterDataEntity>>;
//
//     private sealed class QueryHandler(IMasterDataProvider masterDataProvider) : IQueryHandler<Query, List<IMasterDataEntity>>
//     {
//         public async Task<IQueryResponse<List<IMasterDataEntity>>> HandleAsync(Query request, CancellationToken cancellationToken)
//         {
//             var masterData = await masterDataProvider.GetAsync(
//                 request.ShopfloorKey,
//                 request.Page,
//                 request.PageSize,
//                 request.LastUpdatedAt,
//                 cancellationToken);
//
//             return QueryResponse.OK_200(masterData).Build();
//         }
//     }
// }

