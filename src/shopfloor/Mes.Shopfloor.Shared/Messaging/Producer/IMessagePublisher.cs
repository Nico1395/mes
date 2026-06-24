namespace Mes.Shopfloor.Shared.Messaging.Producer;

public interface IMessagePublisher
{
    Task PublishAsync(IMessage message, CancellationToken cancellationToken);
}