namespace Mes.Shopfloor.Core.Messaging.Producer;

public interface IMessagePublisher
{
    Task PublishAsync(IMessage message, CancellationToken cancellationToken);
}