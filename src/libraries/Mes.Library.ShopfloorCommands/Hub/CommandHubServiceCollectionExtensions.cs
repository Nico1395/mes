using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Hub;

public static class CommandHubServiceCollectionExtensions
{
    public static IServiceCollection AddShopfloorCommandHub(this IServiceCollection services, Action<ShopfloorCommandHubConfigurationBuilder> configuration)
    {
        var builder = new ShopfloorCommandHubConfigurationBuilder();
        configuration(builder);
        var cfg = builder.Build();

        services.AddSingleton(cfg);
        services.AddSignalR().AddStackExchangeRedis(options =>
        {
            options.Configuration.AbortOnConnectFail = false;
            options.Configuration.EndPoints.Add(cfg.RedisUrl ?? throw new InvalidAsynchronousStateException("Redis URL is not configured."));
        });
        services.AddTransient<IShopfloorCommandHubController, ShopfloorCommandHubController>();

        return services;
    }
}