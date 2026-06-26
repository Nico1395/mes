using Mes.Shopfloor.Client.Infrastructure.Routine;
using Mes.Shopfloor.Shared.Contracts.Events;
using Mes.Shopfloor.Shared.Messaging.Producer;

namespace Mes.Shopfloor.Client.ProductionManagement.DataCollection.Jobs;

internal sealed class ReportRejectJob(IMessagePublisher _messagePublisher) : RoutineJob
{
    public override int Order => RoutineJobOrder.ReportReject.ToInt();

    public override async Task ExecuteAsync(IRoutineContext context, CancellationToken cancellationToken)
    {
        var rejectQuantity = context.Get<int?>(RoutineDataKey.RejectQuantity) ?? 0;
        if (rejectQuantity == 0)
            return;

        // Usually the reject would have been supplied first, but since we are faking all data we are just
        // fetching whatever reject we can get our hands on.

        var productionUnitId = context.GetRequired<Guid>(RoutineDataKey. ProductionUnitId);
        var rejectGroup = context.Get<RejectGroupModel>(RoutineDataKey.RejectGroup);
        var @event = new RejectReportedEvent
        {
            ProductionUnitId = productionUnitId,
            RejectQuantity = rejectQuantity,
            RejectId = rejectGroup?.Rejects?.FirstOrDefault()?.Id, // In case of null, we at least can try to log the reject into a fallback reject serverside.
        };

        await _messagePublisher.PublishAsync(@event, cancellationToken);
    }
}