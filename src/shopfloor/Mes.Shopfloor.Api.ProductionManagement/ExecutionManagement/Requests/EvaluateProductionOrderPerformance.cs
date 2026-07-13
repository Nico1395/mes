using DandyMediator;
using DandyMediator.Commands;
using Marten;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Application;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;
using Mes.Shopfloor.Api.SharedKernel.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ExecutionManagement.Requests;

internal static class EvaluateProductionOrderPerformance
{
    internal sealed record Command(Guid ProductionOrderId) : ICommand;

    private sealed class CommandHandler(
        DbContext context,
        IMediator mediator,
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
            var notification = new OrderNotOnTrackV1(
                productionOrderStatus.ProductionOrderId,
                productionOrderStatus.TargetQuantity,
                productionOrderStatus.ProducedQuantity,
                quantityLeftToBeProduced,
                targetQtyPerMin,
                currentQtyPerMin,
                currentDeviation,
                productionOrderStatus.StartedAt.Value,
                productionOrderStatus.ScheduledToCompleteAt,
                projectedCompletionDate
            );

            await mediator.PublishAsync(notification, cancellationToken);
            return CommandResponseFactory.NoContent_204().Build();
        }
    }
}