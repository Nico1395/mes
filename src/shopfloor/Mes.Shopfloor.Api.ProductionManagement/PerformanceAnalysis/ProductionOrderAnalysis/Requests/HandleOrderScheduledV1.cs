using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Requests;

internal static class HandleOrderScheduledV1
{
    private sealed class Consumer(IMediator mediator) : IConsumer<OrderScheduledV1>
    {
        public async Task<ConsumerResult> HandleAsync(OrderScheduledV1 message, CancellationToken cancellationToken)
        {
            var command = new Command(message);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.IsSuccess_2xx() ? ConsumerResult.Ack() : ConsumerResult.Nack();
        }
    }

    private sealed record Command(OrderScheduledV1 OrderScheduled) : ICommand;

    private sealed class CommandHandler(
        IDocumentSession session,
        DbContext context) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var productionOrder = await context.GetProductionOrderByIdAsync(request.OrderScheduled.ProductionOrderId, cancellationToken);
            if (productionOrder == null)
                return CommandResponse.NotFound_404().Build();

            var scheduledProductionOrder = await context.GetScheduledProductionOrderByIdAsync(request.OrderScheduled.ScheduledProductionOrderId, cancellationToken);
            if (scheduledProductionOrder == null)
                return CommandResponse.NotFound_404().Build();
            
            _ = ProductionOrderStatusAggregate.Create(request.OrderScheduled, productionOrder, scheduledProductionOrder);
            
            session.Events.StartStream(productionOrder.Id, request.OrderScheduled);
            await session.SaveChangesAsync(cancellationToken);

            return CommandResponse.Accepted_202().Build();
        }
    }
}