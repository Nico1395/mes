using Mes.Library.SignalR.Connections;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Mes.Library.SignalR;

public static class SignalRServiceCollectionExtensions
{
    public static IServiceCollection AddSignalRWithBackplane(this IServiceCollection services, string? redisUrl, string channelPrefix)
    {
        redisUrl = redisUrl ?? throw new InvalidOperationException("Redis URL is not configured.");

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisUrl));
        services.AddSignalR().AddStackExchangeRedis(options =>
        {
            options.Configuration.AbortOnConnectFail = false;
            options.Configuration.EndPoints.Add(redisUrl);
            options.Configuration.ChannelPrefix = RedisChannel.Literal(channelPrefix);
        });
        services.AddSignalRConnectionManager();

        return services;
    }

    public static IServiceCollection AddSignalRConnectionManager(this IServiceCollection services)
    {
        return services.AddSingleton<ISignalRConnectionManager, SignalRConnectionManager>();
    }
}