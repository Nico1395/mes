using Mes.Shopfloor.Shared.Messaging.Consumer;
using Mes.Shopfloor.Shared.ProductionManagement.Scheduling.Events;

namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Execution.Consumers;

internal sealed class UpdateProdStrategyProdUnitStateChangedConsumer : IConsumer<ProdUnitStateChangedEvent>
{
    public async Task<ConsumerResult> HandleAsync(ProdUnitStateChangedEvent message, CancellationToken cancellationToken)
    {
        // Check whether the new status requires action.
        return ConsumerResult.Ack();
    }
}