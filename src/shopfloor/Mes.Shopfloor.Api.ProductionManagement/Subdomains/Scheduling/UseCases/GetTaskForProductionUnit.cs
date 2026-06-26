using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Contracts;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.Repositories;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.AspNetCore.Mvc;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Scheduling.UseCases;

public static class GetTaskForProductionUnit
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/scheduling/prod-unit-schedules/{productionUnitId:guid}/tasks/current", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "productionUnitId")] Guid productionUnitId,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, ProductionUnitTask>(new Query(productionUnitId), cancellationToken);
                return response.Map(t => t.Map<ProductionUnitTaskDto>()).ToResult();
            });
        }
    }

    private sealed record Query(Guid ProductionUnitId) : IQuery<ProductionUnitTask>;

    private sealed class QueryHandler(IUnitOfWork _unitOfWork) : IQueryHandler<Query, ProductionUnitTask>
    {
        public async Task<IQueryResponse<ProductionUnitTask>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var task = await _unitOfWork
                .Repository<IProductionUnitTaskRepository>()
                .GetTaskForProductionUnitAtPointInTimeAsync(request.ProductionUnitId, now, cancellationToken);

            return task == null
                ? QueryResponseFactory.BadRequest_400<ProductionUnitTask>().Build()
                : QueryResponseFactory.OK_200(task).Build();
        }
    }
}