using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Library.ObjectMapping;
using Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Application;
using Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Requests.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinitionManagement.Requests;

internal static class GetProductionProcessByIdV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/product-definition/production-processes/{id:guid}", async (
                    [FromServices] IMediator mediator,
                    [FromRoute(Name = "id")] Guid id,
                    CancellationToken cancellationToken) =>
                {
                    var query = new Query(id);
                    var response = await mediator.SendAsync<Query, ProductionProcess>(query, cancellationToken);
                    return response.Map(p => p.Map<ProductionProcessHc>()).ToResult();
                });
        }
    }

    private sealed record Query(Guid Id) : IQuery<ProductionProcess>;
    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, ProductionProcess>
    {
        public async Task<IQueryResponse<ProductionProcess>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var process = await context.GetProductionProcessByIdEagerAsync(request.Id, cancellationToken);
            return process.ToResponse();
        }
    }
}