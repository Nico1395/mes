using Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalInitialization;
using Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;
using Mes.Shopfloor.Client.SharedKernel.Configuration;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Connections;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.SharedKernel;

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