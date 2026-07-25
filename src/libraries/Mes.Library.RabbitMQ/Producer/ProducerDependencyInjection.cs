using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.RabbitMQ.Producer;

public static class ProducerDependencyInjection
{
    public static IServiceCollection AddRabbitMQProducer(this IServiceCollection services)
    {
        services.AddScoped<IMessagePublisher, MessagePublisher>();
        
        return services;
    }
}