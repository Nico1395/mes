using DandyMediator.Commands;
using Marten;
using Mes.Library.RabbitMQ.Producer;
using Mes.Shared.Contracts.ProductionData.Orders;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Application;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ExecutionManagement.Requests;

internal static class EvaluateProductionOrderPerformanceV1
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
                return CommandResponse.NotFound_404().Build();

            var productionOrder = await context.GetProductionOrderByIdAsync(request.ProductionOrderId, cancellationToken);
            if (productionOrder == null)
                return CommandResponse.NotFound_404().Build();

            var orderOnTrack = productionOrderStatus.IsOnTrack();
            if (orderOnTrack)
                return CommandResponse.NoContent_204().Build();

            var targetQtyPerMin = productionOrderStatus.GetTargetQuantityPerMinute();
            var currentQtyPerMin = productionOrderStatus.GetCurrentQuantityPerMinute();
            var currentDeviation = targetQtyPerMin - currentQtyPerMin;
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
            return CommandResponse.NoContent_204().Build();
        }
    }
}