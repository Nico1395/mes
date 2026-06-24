namespace Mes.Shopfloor.Shared.Messaging.Consumer;

public interface IConsumer<in TMessage>
    where TMessage : class, IMessage
{
    Task<ConsumerResult> HandleAsync(TMessage message, CancellationToken cancellationToken);
}