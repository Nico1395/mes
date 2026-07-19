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

public static class GetScheduleForProductionUnitV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/scheduling/production-unit-schedules/{productionUnitId:guid}/", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "productionUnitId")] Guid productionUnitId,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, ProductionUnitSchedule>(new Query(productionUnitId), cancellationToken);
                return response.Map(t => t.Map<ProductionUnitScheduleHc>()).ToResult();
            });
        }
    }

    private sealed record Query(Guid ProductionUnitId) : IQuery<ProductionUnitSchedule>;

    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, ProductionUnitSchedule>
    {
        public async Task<IQueryResponse<ProductionUnitSchedule>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var schedule = await context.GetProductionUnitScheduleForProductionUnitAsync(request.ProductionUnitId, cancellationToken);
            return schedule.ToResponse();
        }
    }
}