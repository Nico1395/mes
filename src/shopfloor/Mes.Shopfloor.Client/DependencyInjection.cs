using Mes.Shopfloor.Client.Configuration;
using Mes.Shopfloor.Client.Domains.ProductionManagement;
using Mes.Shopfloor.Client.Domains.ProductionManagement.Subdomains.Resources.Manager;
using Mes.Shopfloor.Client.Infrastructure;
using Mes.Shopfloor.Shared.Messaging.Connections;
using Mes.Shopfloor.Shared.Messaging.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Shopfloor.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddTerminal(this IServiceCollection services, IConfiguration configuration)
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

        services.AddOptions<ProductionUnitOptions>().Bind(configuration.GetSection("ProductionUnit"));
        services.AddOptions<ApiOptions>().Bind(configuration.GetSection("Apis"));
        services.AddOptions<MessageBrokerOptions>().Bind(configuration.GetSection("MessageBroker"));

        var productionManagementUrl = configuration["Apis:ProductionManagementUrl"] ?? throw new InvalidOperationException("No URL of the production management API configured.");
        services.AddHttpClient("pm", cfg =>
        {
            cfg.BaseAddress = new Uri(productionManagementUrl);
        });
        services.AddSingleton<IInitializer, ProductionManagementInitializer>();
        services.AddSingleton<IProductionUnitModelManager, ProductionUnitModelManager>();

        return services;
    }
}