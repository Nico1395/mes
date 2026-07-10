using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.Resources.Application;
using Mes.Shopfloor.Api.ProductionManagement.Resources.Requests.Http;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Mediator;
using Mes.Shopfloor.Shared.SharedKernel.ObjectMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Requests;

internal static class GetWorkerByNumberV1
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/pm/resources/workers/by-number/{number}", async (
                [FromServices] IMediator mediator,
                [FromRoute] string number,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, Worker>(new Query(number), cancellationToken);
                return response.Map(p => p.Map<WorkerDto>()).ToResult();
            });
        }
    }

    private sealed record Query(string Number) : IQuery<Worker>;

    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, Worker>
    {
        public async Task<IQueryResponse<Worker>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var worker = await context.Set<Worker>().AsEager().SingleOrDefaultAsync(w => w.Number == request.Number, cancellationToken);
            return worker.ToResponse();
        }
    }
}