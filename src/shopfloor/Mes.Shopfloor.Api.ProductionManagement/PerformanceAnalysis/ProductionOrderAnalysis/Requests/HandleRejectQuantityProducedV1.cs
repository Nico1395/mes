using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Libraries.RabbitMQ.Consumer;
using Mes.Shared.Events.Quantities;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Requests;

internal static class HandleRejectQuantityProducedV1
{
    private sealed class Consumer(IMediator mediator) : IConsumer<RejectQuantityProducedV1>
    {
        public async Task<ConsumerResult> HandleAsync(RejectQuantityProducedV1 message, CancellationToken cancellationToken)
        {
            var command = new Command(message);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.IsSuccess_2xx() ? ConsumerResult.Ack() : ConsumerResult.Nack();
        }
    }

    private sealed record Command(RejectQuantityProducedV1 RejectQuantityProduced) : ICommand;

    private sealed class CommandHandler(IDocumentSession session) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var status = await session.Events.AggregateStreamAsync<ProductionOrderStatusAggregate>(request.RejectQuantityProduced.ProductionOrderId, token: cancellationToken);
            if (status == null)
                return CommandResponse.NotFound_404().Build();

            status.Apply(request.RejectQuantityProduced);

            session.Events.StartStream(status.ProductionOrderId, request.RejectQuantityProduced);
            await session.SaveChangesAsync(cancellationToken);

            return CommandResponse.Accepted_202().Build();
        }
    }
}