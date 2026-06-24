using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Shared.Messaging.Producer;

public static class ProducerDependencyInjection
{
    public static IServiceCollection AddRabbitMQProducer(this IServiceCollection services)
    {
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        
        return services;
    }
}