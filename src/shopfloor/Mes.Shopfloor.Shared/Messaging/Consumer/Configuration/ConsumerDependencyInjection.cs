using System.Reflection;
using Mes.Shopfloor.Shared.Messaging.Consumer.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Shared.Messaging.Consumer.Configuration;

public static class ConsumerDependencyInjection
{
    private static readonly IReadOnlyList<Type> _consumerTypes =
    [
        typeof(IConsumer<>),
    ];

    public static IServiceCollection AddRabbitMQConsumer(this IServiceCollection services)
    {
        return services.AddRabbitMQConsumer(connectionAction: null);
    }
    
    public static IServiceCollection AddRabbitMQConsumer(this IServiceCollection services, Action<RabbitMQConsumerConfigurationBuilder>? connectionAction)
    {
        var builder = new RabbitMQConsumerConfigurationBuilder();
        connectionAction?.Invoke(builder);
        var connectionConfiguration = builder.Build();
        
        services.AddSingleton(connectionConfiguration);
        services.AddHostedService<ListeningChannelBackgroundService>();

        AddConsumers(services, connectionConfiguration.Assemblies);

        return services;
    }

    private static void AddConsumers(IServiceCollection services, IReadOnlyList<Assembly> assemblies)
    {
        var handlerTypes = assemblies.SelectMany(a => a.DefinedTypes).Where(t => t is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false });
        foreach (var implementationType in handlerTypes)
        {
            var interfaces = implementationType.ImplementedInterfaces;
            foreach (var @interface in interfaces)
            {
                if (!@interface.IsGenericType)
                    continue;

                var genericDefinition = @interface.GetGenericTypeDefinition();
                if (_consumerTypes.Contains(genericDefinition))
                    services.AddTransient(@interface, implementationType);
            }
        }
    }
}