using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Requests;

internal static class HandleMaterialConsumedV1
{
    private sealed class Consumer(IMediator mediator) : IConsumer<MaterialConsumedV1>
    {
        public async Task<ConsumerResult> HandleAsync(MaterialConsumedV1 message, CancellationToken cancellationToken)
        {
            var command = new Command(message);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.IsSuccess_2xx() ? ConsumerResult.Ack() : ConsumerResult.Nack();
        }
    }

    private sealed record Command(MaterialConsumedV1 MaterialConsumed) : ICommand;

    private sealed class CommandHandler(IDocumentSession session) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var status = await session.Events.AggregateStreamAsync<ProductionOrderStatusAggregate>(request.MaterialConsumed.ProductionOrderId, token: cancellationToken);
            if (status == null)
                return CommandResponseFactory.BadRequest_400().Build();

            status.Apply(request.MaterialConsumed);

            session.Events.StartStream(status.ProductionOrderId, request.MaterialConsumed);
            await session.SaveChangesAsync(cancellationToken);

            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}