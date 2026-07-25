using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Library.ObjectMapping;
using Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Application;
using Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Requests.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Requests;

public static class GetProductionUnitByKeyV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/resources/production-units/by-key/{key}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "key")] string key,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, ProductionUnit>(new Query(key), cancellationToken);
                return response.Map(p => p.Map<ProductionUnitDto>()).ToResult();
            });
        }
    }

    private sealed record Query(string Key) : IQuery<ProductionUnit>;
    
    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, ProductionUnit>
    {
        public async Task<IQueryResponse<ProductionUnit>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var productionUnit = await context.GetProductionUnitByKeyEagerAsync(request.Key, cancellationToken);
            return productionUnit.ToResponse();
        }
    }
}