namespace Mes.Shopfloor.Core.Messaging.Consumer;

public interface IConsumption<in TMessage>
    where TMessage : class
{
    Task<ConsumptionResult> HandleAsync(TMessage message, CancellationToken cancellationToken);
}