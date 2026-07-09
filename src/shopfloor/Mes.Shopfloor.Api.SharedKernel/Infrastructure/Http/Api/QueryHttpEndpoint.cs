using DandyEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Http.Api;

public abstract class QueryHttpEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder builder)
    {
        var definition = new EndpointDefinition();
        var @delegate = DefineEndpoint(definition);

        MapEndpoint(builder, definition, @delegate);
    }

    protected virtual void MapEndpoint(IEndpointRouteBuilder builder, EndpointDefinition definition, Delegate @delegate)
    {
        if (!definition.IsSufficientDefinition())
            throw new InvalidOperationException("The endpoint definition is invalid.");

        var handler = builder.MapGet(definition.Uri, @delegate);
        if (!string.IsNullOrWhiteSpace(definition.DisplayName))
            handler.WithDisplayName(definition.DisplayName);

        if (!string.IsNullOrWhiteSpace(definition.Description))
            handler.WithDescription(definition.Description);

        ConfigureEndpoint(handler);
    }

    protected virtual void ConfigureEndpoint(RouteHandlerBuilder routeBuilder)
    {
    }

    protected abstract Delegate DefineEndpoint(EndpointDefinition definition);
}