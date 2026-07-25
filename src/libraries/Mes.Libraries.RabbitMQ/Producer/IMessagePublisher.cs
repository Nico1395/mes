namespace Mes.Libraries.RabbitMQ.Producer;

public interface IMessagePublisher
{
    Task PublishAsync(IMessage message, CancellationToken cancellationToken);
}