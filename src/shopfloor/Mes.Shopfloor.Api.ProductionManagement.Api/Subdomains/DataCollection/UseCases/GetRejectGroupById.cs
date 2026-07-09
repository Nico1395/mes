using DandyEndpoints;
using DandyMediator;
using DandyMediator.Queries;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;
using Mes.Shopfloor.Api.SharedKernel.ProductionManagement.Api.Subdomains.DataCollection.Contracts;
using Mes.Shopfloor.Api.SharedKernel.ProductionManagement.Api.Subdomains.DataCollection.Repositories;
using Mes.Shopfloor.Shared.ObjectMapping;
using Microsoft.AspNetCore.Mvc;

namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.DataCollection.UseCases;

public static class GetRejectGroupById
{
    private sealed class Endpoint : IEndpoint
    {
        public void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/v1/data-collection/reject-group/{rejectGroupId:guid}", async (
                [FromServices] IMediator mediator,
                [FromRoute(Name = "rejectGroupId")] Guid rejectGroupId,
                [FromQuery(Name = "eager")] bool eager,
                CancellationToken cancellationToken) =>
            {
                var query = new Query(rejectGroupId, eager);
                var response = await mediator.SendAsync<Query, RejectGroup>(query, cancellationToken);
                return response.Map(r => r.Map<RejectGroupDto>()).ToResult(); 
            });
        }
    }

    private sealed record Query(Guid RejectGroupId, bool Eager = true) : IQuery<RejectGroup>;
    
    private sealed class QueryHandler(IUnitOfWork _unitOfWork) : IQueryHandler<Query, RejectGroup>
    {
        public async Task<IQueryResponse<RejectGroup>> HandleAsync(Query request, CancellationToken cancellationToken)
        {
            var repository = _unitOfWork.Repository<IRejectGroupRepository>();
            var rejectGroup = request.Eager
                ? await repository.GetByIdEagerAsync(request.RejectGroupId, cancellationToken)
                : await repository.GetByIdAsync(request.RejectGroupId, cancellationToken);

            return rejectGroup == null
                ? QueryResponseFactory.BadRequest_400<RejectGroup>().Build()
                : QueryResponseFactory.OK_200(rejectGroup).Build();
        }
    }
}