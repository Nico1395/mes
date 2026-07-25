using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Libraries.RabbitMQ.Consumer;
using Mes.Libraries.RabbitMQ.Producer;
using Mes.Shared.Events.Orders;
using Mes.Shared.Events.Quantities;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Requests;

internal static class HandleQuantityProducedV1
{
    private sealed class Consumer(IMediator mediator) : IConsumer<QuantityProducedV1>
    {
        public async Task<ConsumerResult> HandleAsync(QuantityProducedV1 message, CancellationToken cancellationToken)
        {
            var command = new Command(message);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.IsSuccess_2xx() ? ConsumerResult.Ack() : ConsumerResult.Nack();
        }
    }

    private sealed record Command(QuantityProducedV1 QuantityProduced) : ICommand;

    private sealed class CommandHandler(
        IMessagePublisher messagePublisher,
        IDocumentSession session) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var status = await session.Events.AggregateStreamAsync<ProductionOrderStatusAggregate>(request.QuantityProduced.ProductionOrderId, token: cancellationToken);
            if (status == null)
                return CommandResponse.NotFound_404().Build();

            var hasBeenCompleted = status.Apply(request.QuantityProduced);
            if (status.HasStarted() && status.IsAbortedOrCompleted() && hasBeenCompleted)
            {
                var orderCompleted = new OrderCompletedV1
                {
                    ProductionOrderId = status.ProductionOrderId,
                    ScheduledToStartAt = status.ScheduledToStartAt,
                    ScheduledToCompleteAt = status.ScheduledToCompleteAt,
                    StartedAt = status.StartedAt.Value,
                    CompletedAt = status.CompletedAt.Value,
                    TargetQuantity = status.TargetQuantity,
                    ProducedQuantity = status.ProducedQuantity,
                    ProducedRejectQuantity = status.ProducedRejectQuantity
                };

                await messagePublisher.PublishAsync(orderCompleted, cancellationToken);
            }

            session.Events.StartStream(request.QuantityProduced.ProductionOrderId, request.QuantityProduced);

            await session.SaveChangesAsync(cancellationToken);
            return CommandResponse.Accepted_202().Build();
        }
    }
}