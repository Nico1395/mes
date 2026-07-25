namespace Mes.Library.ShopfloorCommands.Receiver;

internal sealed class ShopfloorCommandHandler : IShopfloorCommandHandler
{
    public Task HandleAsync(IShopfloorCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}