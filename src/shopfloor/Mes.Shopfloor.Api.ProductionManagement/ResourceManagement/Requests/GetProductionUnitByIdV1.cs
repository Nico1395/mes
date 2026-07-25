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

internal static class GetProductionUnitByIdV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/resources/production-units/{id:guid}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "id")] Guid id,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, ProductionUnit>(new Query(id), cancellationToken);
                return response.Map(p => p.Map<ProductionUnitDto>()).ToResult();
            });
        }
    }

    private sealed record Query(Guid Id) : IQuery<ProductionUnit>;
    
    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, ProductionUnit>
    {
        public async Task<IQueryResponse<ProductionUnit>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var productionUnit = await context.GetProductionUnitByIdEagerAsync(request.Id, cancellationToken);
            return productionUnit.ToResponse();
        }
    }
}