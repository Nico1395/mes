using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Application;
using Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Requests.HttpContracts;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Mediator;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Requests;

// private sealed class HttpEndpoint : QueryHttpEndpoint
// {
//     protected override Delegate DefineEndpoint(EndpointDefinition definition)
//     {
//         definition.Uri = "/api/v1/pm/product-definition/prod-process/{id:guid}";
//         definition.DisplayName = "Map production process by ID";
//         definition.Description = "Finds the production process with they specified ID as its key";
//
//         definition.Produces<ProductionProcessHc>();
//         definition.ProducesProblem(StatusCodes.Status400BadRequest);
//
//         return async (
//             [FromServices] IMediator mediator,
//             [FromRoute(Name = "id")] Guid id,
//             [FromQuery(Name = "eager")] bool eager,
//             CancellationToken cancellationToken) =>
//         {
//             var query = new Query(id, eager);
//             var response = await mediator.SendAsync<Query, ProductionProcess>(query, cancellationToken);
//             return response.Map(p => p.Map<ProductionProcessHc>()).ToResult();
//         };
//     }
// }

internal static class GetProductionProcessByIdV1
{
    [HttpGet("/api/v1/production-process/{id:guid}")]
    [EndpointName("Get production process by ID")]
    [EndpointDescription("Finds the production process with the specified ID as its key.")]
    [Produces<ProductionProcessHc>]
    private static async Task<IResult> HttpEndpoint(
        [FromServices] IMediator mediator,
        [FromRoute(Name = "id")] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new Query(id);
        var response = await mediator.SendAsync<Query, ProductionProcess>(query, cancellationToken);
        return response.Map(p => p.Map<ProductionProcessHc>()).ToResult();
    }

    private sealed record Query(Guid Id) : IQuery<ProductionProcess>;
    private sealed class QueryHandler(DbContext context) : IQueryHandler<Query, ProductionProcess>
    {
        public async Task<IQueryResponse<ProductionProcess>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var process = await context
                .Set<ProductionProcess>()
                .AsEager()
                .SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            return process.ToResponse();
        }
    }
}