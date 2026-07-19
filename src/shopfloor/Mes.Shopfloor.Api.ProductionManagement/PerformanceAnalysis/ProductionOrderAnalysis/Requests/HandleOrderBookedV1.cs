using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Shopfloor.Api.SharedKernel.Domain.Exceptions;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Producer;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Requests;

internal static class HandleOrderBookedV1
{
    private sealed class Consumer(IMediator mediator) : IConsumer<OrderBookedV1>
    {
        public async Task<ConsumerResult> HandleAsync(OrderBookedV1 message, CancellationToken cancellationToken)
        {
            var command = new Command(message);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.IsSuccess_2xx() ? ConsumerResult.Ack() : ConsumerResult.Nack();
        }
    }

    private sealed record Command(OrderBookedV1 OrderBooked) : ICommand;

    private sealed class CommandHandler(
        IMessagePublisher messagePublisher,
        IDocumentSession session) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var success = await TryBookOrderAsync(request.OrderBooked, cancellationToken);
            if (success)
                return CommandResponse.NotFound_404().Build();

            if (request.OrderBooked.PreviousProductionOrderId.HasValue)
            {
                success = await UnbookPreviousOrderAsync(request.OrderBooked.PreviousProductionOrderId.Value, request.OrderBooked, cancellationToken);
                if (!success)
                    return CommandResponse.NotFound_404().Build();
            }

            session.Events.StartStream(request.OrderBooked.ProductionOrderId, request.OrderBooked);
            await session.SaveChangesAsync(cancellationToken);

            return CommandResponse.Accepted_202().Build();
        }

        private async Task<bool> TryBookOrderAsync(OrderBookedV1 orderBooked, CancellationToken cancellationToken)
        {
            var bookedStatus = await session.Events.AggregateStreamAsync<ProductionOrderStatusAggregate>(orderBooked.ProductionOrderId, token: cancellationToken);
            if (bookedStatus == null)
                return false;

            var bookedResult = bookedStatus.Apply(orderBooked);
            DomainRuleViolationException.ThrowIf<OrderBookedV1>(
                bookedResult is OrderBookedResult.Unbooked,
                "Cant unbook an order that is supposed to be booked.");

            return true;
        }

        private async Task<bool> UnbookPreviousOrderAsync(Guid previousProductionOrderId, OrderBookedV1 orderBooked, CancellationToken cancellationToken)
        {
            var previousStatus = await session.Events.AggregateStreamAsync<ProductionOrderStatusAggregate>(previousProductionOrderId, token: cancellationToken);
            if (previousStatus == null)
                return false;

            var bookedResult = previousStatus.Apply(orderBooked);
            DomainRuleViolationException.ThrowIf<OrderBookedV1>(
                bookedResult is not OrderBookedResult.Unbooked,
                "Order should have been unbooked.");

            var unbookedEvent = new OrderUnbookedV1
            {
                ProductionOrderId = previousProductionOrderId,
                NewProductionOrderId =  orderBooked.ProductionOrderId,
                UnbookedAt = orderBooked.OccurredAtUtc,
            };

            await messagePublisher.PublishAsync(unbookedEvent, cancellationToken);
            return true;
        }
    }
}