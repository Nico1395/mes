using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Contracts;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Repositories;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.AspNetCore.Mvc;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.UseCases;

public static class GetProductionUnitByKey
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/resources/prod-unit/key/{key}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "key")] string key,
                [FromQuery(Name = "eager")] bool eager,
                CancellationToken cancellationToken) =>
            {
                var response = await mediator.SendAsync<Query, ProductionUnit>(new Query(key, eager), cancellationToken);
                return response.Map(p => p.Map<ProductionUnitDto>()).ToResult();
            });
        }
    }

    private sealed record Query(string Key, bool Eager = false) : IQuery<ProductionUnit>;
    
    private sealed class QueryHandler(IUnitOfWork _unitOfWork) : IQueryHandler<Query, ProductionUnit>
    {
        public async Task<IQueryResponse<ProductionUnit>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<IProductionUnitRepository>();
            var productionUnit = request.Eager
                ? await repository.GetByKeyEagerAsync(request.Key, cancellationToken)
                : await repository.GetByKeyAsync(request.Key, cancellationToken);
            
            return productionUnit == null
                ? QueryResponseFactory.BadRequest_400<ProductionUnit>().Build()
                : QueryResponseFactory.OK_200(productionUnit).Build();
        }
    }
}