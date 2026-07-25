namespace Mes.Library.ShopfloorCommands.Receiver;

public interface IShopfloorCommandReceiver
{
    Task StartReceivingAsync(CancellationToken cancellationToken);
}