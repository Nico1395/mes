using Mes.Shopfloor.Core.Messaging.Connections;
using Mes.Shopfloor.Core.Messaging.Consumer.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Core.Messaging.Consumer.Configuration;

public static class ConsumerDependencyInjection
{
    public static IServiceCollection AddConsumerConnection(this IServiceCollection services, Action<ConsumerConnectionConfigurationBuilder> connectionAction)
    {
        var builder = new ConsumerConnectionConfigurationBuilder();
        connectionAction(builder);
        var connectionConfiguration = builder.Build();
        
        services.AddSingleton(connectionConfiguration);
        services.AddSingleton<IConnectionProvider, ConnectionProvider>();
        services.AddHostedService<ListeningChannelBackgroundService>();
        
        return services;
    }
}