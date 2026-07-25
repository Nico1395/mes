namespace Mes.Library.ShopfloorCommands.Receiver;

public interface IShopfloorCommandHandler
{
    Task HandleAsync(IShopfloorCommand command, CancellationToken cancellationToken);
}

public interface IShopfloorCommandHandler<in TCommand>
    where TCommand : class, IShopfloorCommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}