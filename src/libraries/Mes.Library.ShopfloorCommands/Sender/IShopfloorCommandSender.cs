namespace Mes.Library.ShopfloorCommands.Sender;

public interface IShopfloorCommandSender
{
    Task<ShopfloorCommandResponse> SendAsync(IShopfloorCommand command, CancellationToken cancellationToken);
    Task<ShopfloorCommandResponse> BroadcastAsync(IShopfloorCommand command, CancellationToken cancellationToken);
}