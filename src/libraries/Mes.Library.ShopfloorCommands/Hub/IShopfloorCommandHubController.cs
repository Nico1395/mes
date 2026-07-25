namespace Mes.Library.ShopfloorCommands.Hub;

public interface IShopfloorCommandHubController
{
    Task<ShopfloorCommandResponse> SendAsync(IShopfloorCommand command, CancellationToken cancellationToken);
    Task<ShopfloorCommandResponse> BroadcastAsync(IShopfloorCommand command, CancellationToken cancellationToken);
}