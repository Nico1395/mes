using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Core.Messaging.Connections;

public static class ConnectionDependencyInjection
{
    public static IServiceCollection AddRabbitMQConnection(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionProvider, ConnectionProvider>();
        
        return services;
    }
}