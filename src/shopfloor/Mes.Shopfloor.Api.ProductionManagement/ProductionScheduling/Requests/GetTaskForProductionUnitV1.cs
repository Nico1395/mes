using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Requests.Http;
using Mes.Shopfloor.Shared.SharedKernel.ObjectMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Requests;

public static class GetTaskForProductionUnitV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/scheduling/production-unit-schedules/{productionUnitId:guid}/tasks/current", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "productionUnitId")] Guid productionUnitId,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, ProductionUnitTask>(new Query(productionUnitId), cancellationToken);
                return response.Map(t => t.Map<ProductionUnitTaskHc>()).ToResult();
            });
        }
    }

    private sealed record Query(Guid ProductionUnitId) : IQuery<ProductionUnitTask>;

    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, ProductionUnitTask>
    {
        public async Task<IQueryResponse<ProductionUnitTask>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var task = await context.GetProductionUnitTaskForProductionUnitAtPointInTimeAsync(
                request.ProductionUnitId,
                DateTime.UtcNow,
                cancellationToken);

            return task.ToResponse();
        }
    }
}