using DandyMediator.Commands;
using Marten;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Application;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Producer;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ExecutionManagement.Requests;

internal static class EvaluateProductionOrderPerformance
{
    internal sealed record Command(Guid ProductionOrderId) : ICommand;

    private sealed class CommandHandler(
        DbContext context,
        IMessagePublisher messagePublisher,
        IDocumentSession session) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var productionOrderStatus = await session.GetProductionOrderStatusByIdAsync(request.ProductionOrderId, cancellationToken);
            if (productionOrderStatus == null || !productionOrderStatus.HasStarted())
                return CommandResponseFactory.NoContent_204().Build();

            var productionOrder = await context.GetProductionOrderByIdAsync(request.ProductionOrderId, cancellationToken);
            if (productionOrder == null)
                return CommandResponseFactory.NoContent_204().Build();

            var targetQtyPerMin = productionOrderStatus.GetTargetQuantityPerMinute();
            var currentQtyPerMin = productionOrderStatus.GetCurrentQuantityPerMinute();
            var currentDeviation = targetQtyPerMin - currentQtyPerMin;
            var orderOnTrack = currentDeviation / targetQtyPerMin * 100 <= productionOrder.AcceptableDeviationPercent;
            if (orderOnTrack)
                return CommandResponseFactory.NoContent_204().Build();

            var quantityLeftToBeProduced = productionOrderStatus.GetQuantityLeftToBeProduced();
            var projectedCompletionDate = productionOrderStatus.GetProjectedCompletionDate();
            var notOnTrack = new OrderNotOnTrackV1
            {
                ProductionOrderId = productionOrderStatus.ProductionOrderId,
                TargetQuantity = productionOrderStatus.TargetQuantity,
                ProducedQuantity = productionOrderStatus.ProducedQuantity,
                QuantityLeftToBeProduced = quantityLeftToBeProduced,
                TargetQuantityPerMinute = targetQtyPerMin,
                CurrentQuantityPerMinute = currentQtyPerMin,
                CurrentDeviation = currentDeviation,
                StartedAt = productionOrderStatus.StartedAt.Value,
                ScheduledToCompleteAt = productionOrderStatus.ScheduledToCompleteAt,
                ProjectedCompletionDate = projectedCompletionDate,
            };

            await messagePublisher.PublishAsync(notOnTrack, cancellationToken);
            return CommandResponseFactory.NoContent_204().Build();
        }
    }
}