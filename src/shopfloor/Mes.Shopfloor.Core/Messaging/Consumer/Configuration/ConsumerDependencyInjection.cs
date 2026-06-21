using Mes.Shopfloor.Core.Messaging.Consumer.Connection;
using Mes.Shopfloor.Core.Messaging.Consumer.ListeningRoutine;
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
        services.AddSingleton<IConsumerConnectionProvider>(_ => new ConsumerConnectionProvider(connectionConfiguration.ConnectionFactory));
        services.AddHostedService<ConsumerBackgroundService>();
        
        return services;
    }
}