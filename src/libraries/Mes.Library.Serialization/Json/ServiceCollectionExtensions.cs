using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.Serialization.Json;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMinimalApiJsonOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options => { options.SerializerOptions.TypeInfoResolver = MesJsonSerializer.CreateTypeInfoResolver(); });

        return services;
    }
}