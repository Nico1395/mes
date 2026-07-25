namespace Mes.Library.RabbitMQ.Producer;

public interface IMessagePublisher
{
    Task PublishAsync(IMessage message, CancellationToken cancellationToken);
}