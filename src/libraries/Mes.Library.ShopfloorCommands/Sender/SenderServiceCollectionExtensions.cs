using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Sender;

public static class SenderServiceCollectionExtensions
{
    public static IServiceCollection AddShopfloorCommandSender(this IServiceCollection services, Action<ShopfloorCommandSenderConfigurationBuilder> configuration)
    {
        var builder = new ShopfloorCommandSenderConfigurationBuilder();
        configuration(builder);
        var cfg = builder.Build();

        services.AddSingleton(cfg);
        services.AddSignalR().AddStackExchangeRedis(options =>
        {
            options.Configuration.AbortOnConnectFail = false;
            options.Configuration.EndPoints.Add(cfg.RedisUrl ?? throw new InvalidAsynchronousStateException("Redis URL is not configured."));
        });
        services.AddTransient<IShopfloorCommandSender, ShopfloorCommandSender>();

        return services;
    }
}