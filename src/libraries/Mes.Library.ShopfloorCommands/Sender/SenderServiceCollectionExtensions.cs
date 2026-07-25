using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Sender;

public static class SenderServiceCollectionExtensions
{
    public static IServiceCollection AddShopfloorCommandSender(this IServiceCollection services)
    {
        services.AddSingleton<IShopfloorCommandSender, ShopfloorCommandSender>();

        return services;
    }
}