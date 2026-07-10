namespace Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer;

public interface IConsumer<in TMessage>
    where TMessage : class, IMessage
{
    Task<ConsumerResult> HandleAsync(TMessage message, CancellationToken cancellationToken);
}