using Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Producer;

namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Jobs;

internal sealed class ReportQuantitiesJob(IMessagePublisher _messagePublisher) : TerminalRoutineJob
{
    public override int Order => JobOrder.ReportQuantities.ToInt();

    public override async Task ExecuteAsync(ITerminalRoutineContext context, CancellationToken cancellationToken)
    {
        var productionUnitId = context.GetRequired<Guid>(DataKey.ProductionUnitId);
        var orderId = context.GetRequired<Guid>(DataKey.OrderId);
        var workerId = context.GetRequired<Guid>(DataKey.WorkerId);

        var quantityProduced = new QuantityProducedV1
        {
            ProductionUnitId = productionUnitId,
            WorkerId = workerId,
            ProductionOrderId = orderId,
            ProducedQuantity = context.GetRequired<double>(DataKey.ProducedQuantity),
        };
        await _messagePublisher.PublishAsync(quantityProduced, cancellationToken);

        var rejectQuantity = context.Get<int?>(DataKey.RejectQuantity) ?? 0;
        if (rejectQuantity == 0)
            return;

        var rejectGroup = context.Get<RejectGroupModel>(DataKey.RejectGroup);
        var rejectProduced = new RejectQuantityProducedV1
        {
            ProductionUnitId = productionUnitId,
            WorkerId = workerId,
            ProductionOrderId = orderId,
            ProducedRejectQuantity = rejectQuantity,
            RejectId = rejectGroup?.Rejects?.FirstOrDefault()?.Id, // In case of null, we at least can try to log the reject into a fallback reject serverside.
        };
        await _messagePublisher.PublishAsync(rejectProduced, cancellationToken);
    }
}