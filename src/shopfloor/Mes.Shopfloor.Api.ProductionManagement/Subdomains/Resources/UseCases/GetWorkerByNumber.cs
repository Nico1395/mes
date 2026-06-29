using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Contracts;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Repositories;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.AspNetCore.Mvc;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.UseCases;

public static class GetWorkerByNumber
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/resources/worker/Number/{Number}", async (
                [FromServices] IMediator mediator,
                [FromRoute] string number,
                [FromQuery] bool eager,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, Worker>(new Query(number, eager), cancellationToken);
                return response.Map(p => p.Map<WorkerDto>()).ToResult();
            });
        }
    }

    private sealed record Query(string Number, bool Eager) : IQuery<Worker>;

    private sealed class QueryHandler(IWorkerRepository _repository) : IQueryHandler<Query, Worker>
    {
        public async Task<IQueryResponse<Worker>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var worker = request.Eager
                ? await _repository.GetByNumberEagerAsync(request.Number, cancellationToken)
                : await _repository.GetByNumberAsync(request.Number, cancellationToken);

            return worker == null
                ? QueryResponseFactory.BadRequest_400<Worker>().Build()
                : QueryResponseFactory.OK_200(worker).Build();
        }
    }
}