using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.UseCases;

public static class ProductionUnitExists
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/resources/prod-unit/{id:guid}/exist", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "id")] Guid id,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, bool>(new Query(id), cancellationToken);
                return response.ToResult();
            });
        }
    }

    public sealed record Query(Guid ProductionUnitId) : IQuery<bool>;

    private sealed class QueryHandler(IUnitOfWork _unitOfWork) : IQueryHandler<Query, bool>
    {
        public async Task<IQueryResponse<bool>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var exists = await _unitOfWork.Repository<IProductionUnitRepository>().ExistsAsync(request.ProductionUnitId, cancellationToken);
            return QueryResponseFactory.OK_200(exists).Build();
        }
    }
}