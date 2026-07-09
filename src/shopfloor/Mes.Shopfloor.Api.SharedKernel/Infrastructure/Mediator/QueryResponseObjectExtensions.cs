using DandyMediator.Queries;

namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Mediator;

public static class QueryResponseObjectExtensions
{
    public static IQueryResponse<T> ToResponse<T>(this T? item)
    {
        return item == null ? QueryResponseFactory.BadRequest_400<T>().Build() : QueryResponseFactory.OK_200(item).Build();
    }
}