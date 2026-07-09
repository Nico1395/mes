namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Modules;

public interface IRequestMap<TRequest, TResponse, TMapRequest, TMapResponse>
{
    TRequest Map(TMapRequest request);
    TMapResponse Map(TResponse request);
}
