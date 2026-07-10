using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Requests.Http;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Mediator;
using Mes.Shopfloor.Shared.SharedKernel.ObjectMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Requests;

public static class GetProductionOrderByIdV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/scheduling/production-orders/{id:guid}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "id")] Guid id,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, ProductionOrder>(new Query(id), cancellationToken);
                return response.Map(t => t.Map<ProductionOrderHc>()).ToResult();
            });
        }
    }

    private sealed record Query(Guid ProductionOrderId) : IQuery<ProductionOrder>;

    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, ProductionOrder>
    {
        public async Task<IQueryResponse<ProductionOrder>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var order = await context.GetProductionOrderByIdEagerAsync(request.ProductionOrderId, cancellationToken);
            return order.ToResponse();
        }
    }
}