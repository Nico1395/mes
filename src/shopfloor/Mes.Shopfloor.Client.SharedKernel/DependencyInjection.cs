using System.Reflection;
using Mes.Library.RabbitMQ.Connections;
using Mes.Library.RabbitMQ.Producer;
using Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalInitialization;
using Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;
using Mes.Shopfloor.Client.SharedKernel.Configuration;
using Mes.Shopfloor.Client.SharedKernel.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client.SharedKernel;

public static class DependencyInjection
{
    public static IServiceCollection AddTerminalCore(this IServiceCollection services, IConfiguration configuration, Assembly[] assemblies)
    {
        var brokerUserName = configuration["MessageBroker:UserName"] ?? throw new InvalidOperationException("No message broker user name configured.");
        var brokerPassword = configuration["MessageBroker:Password"] ?? throw new InvalidOperationException("No message broker user password configured.");
        var brokerNodes = configuration.GetSection("MessageBroker:Nodes").Get<string[]>() ?? throw new InvalidOperationException("No message broker nodes configured.");

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

        var apiUrl = configuration["Apis:ApiUrl"] ?? throw new InvalidOperationException("No URL of the production management API configured.");
        services.AddHttpClient(HttpClientConstants.ApiHttpClientName, client =>
        {
            client.BaseAddress = new Uri(apiUrl);
        });

        return services;
    }
}