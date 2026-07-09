using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;
using Mes.Shopfloor.Api.SharedKernel.ProductionManagement.Api.Subdomains.Scheduling.Contracts;
using Mes.Shopfloor.Api.SharedKernel.ProductionManagement.Api.Subdomains.Scheduling.Repositories;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.AspNetCore.Mvc;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Scheduling.UseCases;

public static class GetScheduleForProductionUnit
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/scheduling/prod-unit-schedules/{productionUnitId:guid}/", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "productionUnitId")] Guid productionUnitId,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, ProductionUnitSchedule>(new Query(productionUnitId), cancellationToken);
                return response.Map(t => t.Map<ProductionUnitScheduleDto>()).ToResult();
            });
        }
    }

    private sealed record Query(Guid ProductionUnitId) : IQuery<ProductionUnitSchedule>;

    private sealed class QueryHandler(IUnitOfWork _unitOfWork) : IQueryHandler<Query, ProductionUnitSchedule>
    {
        public async Task<IQueryResponse<ProductionUnitSchedule>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var schedule = await _unitOfWork.Repository<IProductionUnitScheduleRepository>().GetForProductionUnitAsync(request.ProductionUnitId, cancellationToken);
            return schedule == null
                ? QueryResponseFactory.BadRequest_400<ProductionUnitSchedule>().Build()
                : QueryResponseFactory.OK_200(schedule).Build();
        }
    }
}