namespace Mes.Shopfloor.Shared.SharedKernel.Messaging.Producer;

public interface IMessagePublisher
{
    Task PublishAsync(IMessage message, CancellationToken cancellationToken);
}