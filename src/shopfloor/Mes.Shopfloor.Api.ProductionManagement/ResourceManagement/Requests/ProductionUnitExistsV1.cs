using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Application;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Requests;

internal static class ProductionUnitExistsV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/resources/production-units/{id:guid}/exist", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "id")] Guid id,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, bool>(new Query(id), cancellationToken);
                return response.ToResult();
            });
        }
    }

    private sealed record Query(Guid Id) : IQuery<bool>;

    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, bool>
    { 
        public async Task<IQueryResponse<bool>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var exists = await context.ProductionUnitExistsAsync(request.Id, cancellationToken);
            return exists.ToResponse();
        }
    }
}