using Mes.Hub.Edge.Synchronization.MasterData.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Hub.Edge.Synchronization;

public static class DependencyInjection
{
    public static IServiceCollection AddMesHubEdgeSynchronization(this IServiceCollection services)
    {
        services.AddSingleton<MasterDataTypeResolver>();
        services.AddScoped<IMasterDataProvider, MasterDataProvider>();

        return services;
    }
}