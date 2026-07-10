using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Application;
using Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Requests.Http;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Http.Api;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Mediator;
using Mes.Shopfloor.Shared.SharedKernel.ObjectMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductDefinition.Requests;

internal static class GetProductionProcessByIdV1
{
    // [HttpGet("/api/v1/pm/product-definition/production-processes/{id:guid}")]
    // [EndpointName("Get production process by ID")]
    // [EndpointDescription("Finds the production process with the specified ID as its key.")]
    // [Produces<ProductionProcessHc>]
    // private static async Task<IResult> HttpEndpoint(
    //     [FromServices] IMediator mediator,
    //     [FromRoute(Name = "id")] Guid id,
    //     CancellationToken cancellationToken)
    // {
    //     var query = new Query(id);
    //     var response = await mediator.SendAsync<Query, ProductionProcess>(query, cancellationToken);
    //     return response.Map(p => p.Map<ProductionProcessHc>()).ToResult();
    // }

    // private sealed class HttpEndpoint : QueryHttpEndpoint
    // {
    //     protected override Delegate DefineEndpoint(EndpointDefinition definition)
    //     {
    //         definition.Uri = "/api/v1/pm/product-definition/production-processes/{id:guid}";
    //         definition.DisplayName = "Map production process by ID";
    //         definition.Description = "Finds the production process with they specified ID as its key";
    //
    //         // definition.Produces<ProductionProcessHc>();
    //         // definition.ProducesProblem(StatusCodes.Status400BadRequest);
    //
    //         return async (
    //             [FromServices] IMediator mediator,
    //             [FromRoute(Name = "id")] Guid id,
    //             CancellationToken cancellationToken) =>
    //         {
    //             var query = new Query(id);
    //             var response = await mediator.SendAsync<Query, ProductionProcess>(query, cancellationToken);
    //             return response.Map(p => p.Map<ProductionProcessHc>()).ToResult();
    //         };
    //     }
    // }
    
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