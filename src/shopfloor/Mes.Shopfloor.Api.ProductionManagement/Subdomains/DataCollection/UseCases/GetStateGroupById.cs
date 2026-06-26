using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.Infrastructure;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Contracts;
using Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Repositories;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.AspNetCore.Mvc;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.UseCases;

public static class GetStateGroupById
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/data-collection/state-group/{stateGroupId:guid}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "stateGroupId")] Guid stateGroupId,
                [FromQuery(Name = "eager")] bool eager,
                CancellationToken cancellationToken) =>
            {
                var query = new Query(stateGroupId, eager);
                var response = await mediator.SendAsync<Query, StateGroup>(query, cancellationToken);
                return response.Map(r => r.Map<StateGroupDto>()).ToResult(); 
            });
        }
    }

    private sealed record Query(Guid ProductionStateGroupId, bool Eager = true) : IQuery<StateGroup>;
    
    private sealed class QueryHandler(IUnitOfWork _unitOfWork) : IQueryHandler<Query, StateGroup>
    {
        public async Task<IQueryResponse<StateGroup>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<IStateGroupRepository>();
            var group = request.Eager
                ? await repository.GetByIdEagerAsync(request.ProductionStateGroupId, cancellationToken)
                : await repository.GetByIdAsync(request.ProductionStateGroupId, cancellationToken);

            return group == null
                ? QueryResponseFactory.BadRequest_400<StateGroup>().Build()
                : QueryResponseFactory.OK_200(group).Build();
        }
    }
}