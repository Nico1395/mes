using Mes.Library.Serialization.Json;
using Mes.Library.SignalR.Connections;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Mes.Library.SignalR;

public static class SignalRServiceCollectionExtensions
{
    public static IServiceCollection AddSignalRWithBackplane(this IServiceCollection services, string? redisUrl, string channelPrefix)
    {
        redisUrl = redisUrl ?? throw new InvalidOperationException("Redis URL is not configured.");

        services.AddSingleton<ISignalRConnectionManager, SignalRConnectionManager>();
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisUrl));
        services.AddSignalR()
            .AddStackExchangeRedis(options =>
            {
                options.Configuration.AbortOnConnectFail = false;
                options.Configuration.EndPoints.Add(redisUrl);
                options.Configuration.ChannelPrefix = RedisChannel.Literal(channelPrefix);
            })
            .AddJsonProtocol(options => { options.PayloadSerializerOptions.TypeInfoResolver = MesJsonSerializer.CreateTypeInfoResolver(); });

        return services;
    }
}