using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Shared.Messaging.Connections;

public static class ConnectionDependencyInjection
{
    public static IServiceCollection AddRabbitMQConnection(this IServiceCollection services, Action<RabbitMQConnectionConfigurationBuilder>? connectionAction)
    {
        var builder = new RabbitMQConnectionConfigurationBuilder();
        connectionAction?.Invoke(builder);
        var connectionConfiguration = builder.Build();

        services.AddSingleton(connectionConfiguration);
        services.AddSingleton<IConnectionProvider, ConnectionProvider>();

        return services;
    }

    public static IServiceCollection AddRabbitMQConnection(this IServiceCollection services)
    {
        return services.AddRabbitMQConnection(connectionAction: null);
    }
}