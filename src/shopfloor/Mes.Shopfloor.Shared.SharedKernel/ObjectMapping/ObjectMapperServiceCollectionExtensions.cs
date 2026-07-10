using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Shared.SharedKernel.ObjectMapping;

public static class ObjectMapperServiceCollectionExtensions
{
    public static IServiceCollection AddObjectMapper(this IServiceCollection services)
    {
        services.AddMapster();
        services.AddTransient<IObjectMapper, ObjectMapper>();
        
        return services;
    }
}