using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Contracts;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.Repositories;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.AspNetCore.Mvc;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.ProductDefinition.UseCases;

public static class GetProductionProcessById
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/product-definition/prod-process/{id:guid}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "id")] Guid id,
                [FromQuery(Name = "eager")] bool eager,
                CancellationToken cancellationToken) =>
            {
                var query = new Query(id, eager);
                var response = await mediator.SendAsync<Query, ProductionProcess>(query, cancellationToken);
                return response.Map(p => p.Map<ProductionProcessDto>()).ToResult();
            });
        }
    }

    private sealed record Query(Guid ProductionProcessId, bool Eager = true) : IQuery<ProductionProcess>;

    private sealed class QueryHandler(IProductionProcessRepository _productionProcessRepository) : IQueryHandler<Query, ProductionProcess>
    {
        public async Task<IQueryResponse<ProductionProcess>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var process = request.Eager
                ? await _productionProcessRepository.GetByIdEagerAsync(request.ProductionProcessId, cancellationToken)
                : await _productionProcessRepository.GetByIdAsync(request.ProductionProcessId, cancellationToken);

            return process == null
                ? QueryResponseFactory.BadRequest_400<ProductionProcess>().Build()
                : QueryResponseFactory.OK_200(process).Build();
        }
    }
}