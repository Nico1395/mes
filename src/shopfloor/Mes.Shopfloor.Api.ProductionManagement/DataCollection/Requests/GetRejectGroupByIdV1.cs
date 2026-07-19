using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;
using Mes.Shopfloor.Api.ProductionManagement.DataCollection.Requests.Http;
using Mes.Shopfloor.Shared.SharedKernel.ObjectMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Requests;

public static class GetRejectGroupByIdV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/data-collection/reject-groups/{id:guid}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "id")] Guid id,
                CancellationToken cancellationToken) =>
            {
                var query = new Query(id);
                var response = await mediator.SendAsync<Query, RejectGroup>(query, cancellationToken);
                return response.Map(r => r.Map<RejectGroupHc>()).ToResult(); 
            });
        }
    }

    private sealed record Query(Guid Id) : IQuery<RejectGroup>;

    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, RejectGroup>
    {
        public async Task<IQueryResponse<RejectGroup>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var rejectGroup = await context.GetRejectGroupByIdEagerAsync(request.Id, cancellationToken);
            return rejectGroup.ToResponse();
        }
    }
}