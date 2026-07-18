using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Requests;

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
                return CommandResponseFactory.BadRequest_400().Build();

            status.Apply(request.RejectQuantityProduced);

            session.Events.StartStream(status.ProductionOrderId, request.RejectQuantityProduced);
            await session.SaveChangesAsync(cancellationToken);

            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}