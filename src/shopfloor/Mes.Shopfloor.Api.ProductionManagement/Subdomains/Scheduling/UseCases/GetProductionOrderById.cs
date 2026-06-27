using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Contracts;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Repositories;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.AspNetCore.Mvc;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.UseCases;

public static class GetProductionOrderById
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/scheduling/prod-order/{id:guid}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "id")] Guid id,
                [FromQuery(Name = "eager")] bool eager,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, ProductionOrder>(new Query(id), cancellationToken);
                return response.Map(t => t.Map<ProductionOrderDto>()).ToResult();
            });
        }
    }

    private sealed record Query(Guid ProductionOrderId, bool Eager = true) : IQuery<ProductionOrder>;

    private sealed class QueryHandler(IProductionOrderRepository _productionOrderRepository) : IQueryHandler<Query, ProductionOrder>
    {
        public async Task<IQueryResponse<ProductionOrder>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var schedule = request.Eager
                ? await _productionOrderRepository.GetByIdEagerAsync(request.ProductionOrderId, cancellationToken)
                : await _productionOrderRepository.GetByIdAsync(request.ProductionOrderId, cancellationToken);

            return schedule == null
                ? QueryResponseFactory.BadRequest_400<ProductionOrder>().Build()
                : QueryResponseFactory.OK_200(schedule).Build();
        }
    }
}