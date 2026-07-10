using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;
using Mes.Shopfloor.Api.ProductionManagement.DataCollection.Requests.Http;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Mediator;
using Mes.Shopfloor.Shared.SharedKernel.ObjectMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Requests;

public static class GetStateGroupByIdV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/data-collection/state-groups/{stateGroupId:guid}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "stateGroupId")] Guid stateGroupId,
                CancellationToken cancellationToken) =>
            {
                var query = new Query(stateGroupId);
                var response = await mediator.SendAsync<Query, StateGroup>(query, cancellationToken);
                return response.Map(r => r.Map<StateGroupHc>()).ToResult(); 
            });
        }
    }

    private sealed record Query(Guid Id) : IQuery<StateGroup>;
    
    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, StateGroup>
    {
        public async Task<IQueryResponse<StateGroup>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var group = await context.GetStateGroupByIdEagerAsync(request.Id, cancellationToken);
            return group.ToResponse();
        }
    }
}