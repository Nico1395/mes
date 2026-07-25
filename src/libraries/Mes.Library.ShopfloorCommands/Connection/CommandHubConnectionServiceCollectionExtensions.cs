using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Connection;

public static class CommandHubConnectionServiceCollectionExtensions
{
    public static IServiceCollection AddShopfloorCommandHubConnection(this IServiceCollection services, Action<ShopfloorCommandHubConnectionReceiverConfigurationBuilder> configuration)
    {
        var builder = new ShopfloorCommandHubConnectionReceiverConfigurationBuilder();
        configuration(builder);
        var cfg = builder.Build();

        services.AddSingleton(cfg);
        services.AddSingleton<IShopfloorCommandHubConnectionFactory, ShopfloorCommandHubConnectionFactory>();
        services.AddSingleton<IShopfloorCommandHubConnectionProvider, ShopfloorCommandHubConnectionHubConnectionProvider>();

        return services;
    }
}