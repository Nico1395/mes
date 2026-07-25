using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Sender;

public static class SenderServiceCollectionExtensions
{
    public static IServiceCollection AddShopfloorCommandSender(this IServiceCollection services)
    {
        // Cors!
        services.AddSignalR();
        services.AddTransient<IShopfloorCommandSender, ShopfloorCommandSender>();

        return services;
    }
}