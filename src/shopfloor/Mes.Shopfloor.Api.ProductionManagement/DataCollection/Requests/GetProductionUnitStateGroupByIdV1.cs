using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Library.ObjectMapping;
using Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;
using Mes.Shopfloor.Api.ProductionManagement.DataCollection.Requests.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Requests;

public static class GetProductionUnitStateGroupByIdV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/data-collection/production-unit-state-groups/{id:guid}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "id")] Guid id,
                CancellationToken cancellationToken) =>
            {
                var query = new Query(id);
                var response = await mediator.SendAsync<Query, ProductionUnitStateGroup>(query, cancellationToken);
                return response.Map(r => r.Map<StateGroupHc>()).ToResult(); 
            });
        }
    }

    private sealed record Query(Guid Id) : IQuery<ProductionUnitStateGroup>;

    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, ProductionUnitStateGroup>
    {
        public async Task<IQueryResponse<ProductionUnitStateGroup>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var group = await context.GetProductionUnitStateGroupByIdEagerAsync(request.Id, cancellationToken);
            return group.ToResponse();
        }
    }
}