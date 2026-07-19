using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;

namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Http.Api;

public sealed class EndpointDefinition
{
    [StringSyntax("Route")]
    public string? Uri { get; set; }
    public string? DisplayName { get; set; }
    public string? Description { get; set; }

    // public List<IProducesResponseTypeMetadata> ProducesMetadata { get; set; } = [];

    // public EndpointDefinition Produces<TResponse>(int statusCode = StatusCodes.Status200OK)
    // {
    //     
    // }
    //
    // public EndpointDefinition Produces(int statusCode = StatusCodes.Status200OK, Type? responseType = null)
    // {
    //     
    // }
    //
    // public EndpointDefinition ProducesProblem(int statusCode)
    // {
    //     
    // }
    //
    // public EndpointDefinition ProducesValidationProblem(int statusCode = StatusCodes.Status400BadRequest)
    // {
    //     
    // }

    [MemberNotNullWhen(true, nameof(Uri))]
    public bool IsSufficientDefinition()
    {
        return !string.IsNullOrWhiteSpace(Uri);
    }
}