using Mes.Shopfloor.Client.Configuration;
using Mes.Shopfloor.Client.Infrastructure.Initialization;
using Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;
using Mes.Shopfloor.Shared.Messaging.Connections;
using Mes.Shopfloor.Shared.Messaging.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddTerminalCore(this IServiceCollection services, IConfiguration configuration)
    {
        var brokerUserName = configuration["MessageBroker:UserName"] ?? throw new InvalidOperationException("No message broker user name configured.");
        var brokerPassword = configuration["MessageBroker:Password"] ?? throw new InvalidOperationException("No message broker user password configured.");
        var brokerNodes = configuration.GetSection("MessageBroker:Nodes").Get<string[]>() ?? throw new InvalidOperationException("No message broker nodes configured.");

        var assemblies = new[] { typeof(DependencyInjection).Assembly, };

        services.AddRabbitMQConnection(connection =>
        {
            connection.ConnectToCluster(
                userName: brokerUserName,
                password: brokerPassword,
                nodes: brokerNodes);
        });
        services.AddRabbitMQProducer();
        services.AddTerminalInitialization(assemblies);
        services.AddTerminalRoutine(assemblies);

        services.AddOptions<ProductionUnitOptions>().Bind(configuration.GetSection("ProductionUnit"));
        services.AddOptions<ApiOptions>().Bind(configuration.GetSection("Apis"));
        services.AddOptions<MessageBrokerOptions>().Bind(configuration.GetSection("MessageBroker"));
        services.AddOptions<RoutineOptions>().Bind(configuration.GetSection("TerminalRoutine"));

        return services;
    }
}