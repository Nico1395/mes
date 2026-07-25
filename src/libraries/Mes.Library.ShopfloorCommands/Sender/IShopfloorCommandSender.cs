namespace Mes.Library.ShopfloorCommands.Sender;

public interface IShopfloorCommandSender
{
    Task<ShopfloorCommandResponse> SendAsync(IShopfloorToShopfloorCommand command, CancellationToken cancellationToken);
}