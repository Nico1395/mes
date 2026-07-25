using Mes.Library.RabbitMQ.Producer;

namespace Mes.Library.ShopfloorCommands.Receiver;

internal sealed class ShopfloorCommandHandler(IMessagePublisher messagePublisher) : IShopfloorCommandHandler
{
    public Task HandleAsync(IShopfloorCommand command, CancellationToken cancellationToken)
    {
        return messagePublisher.PublishAsync(command, cancellationToken);
    }
}