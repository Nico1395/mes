using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Http.Api;

public static class EndpointRouteBuilder
{
    public static IEndpointConventionBuilder MapQuery(
        this IEndpointRouteBuilder endpoints,
        string displayName,
        string description,
        [StringSyntax("Route")] string pattern,
        Delegate @delegate)
    {
        return endpoints
            .MapGet(pattern, @delegate)
            .WithDisplayName(displayName)
            .WithSummary(description)
            .WithTags("query")
            .RequireAuthorization();
    }

    public static IEndpointConventionBuilder MapCommand(
        this IEndpointRouteBuilder endpoints,
        string displayName,
        string description,
        [StringSyntax("Route")] string pattern,
        Delegate @delegate)
    {
        return endpoints
            .MapPost(pattern, @delegate)
            .WithDisplayName(displayName)
            .WithSummary(description)
            .WithTags("command")
            .RequireAuthorization();
    }
}