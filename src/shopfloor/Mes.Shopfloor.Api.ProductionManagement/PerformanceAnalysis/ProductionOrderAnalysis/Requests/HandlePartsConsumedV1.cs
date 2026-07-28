using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Library.RabbitMQ.Consumer;
using Mes.Shared.Contracts.SharedKernel.ProductionData.Events.MaterialsAndParts;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Requests;

internal static class HandlePartsConsumedV1
{
    private sealed class Consumer(IMediator mediator) : IConsumer<PartsConsumedV1>
    {
        public async Task<ConsumerResult> HandleAsync(PartsConsumedV1 message, CancellationToken cancellationToken)
        {
            var command = new Command(message);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.IsSuccess_2xx() ? ConsumerResult.Ack() : ConsumerResult.Nack();
        }
    }

    private sealed record Command(PartsConsumedV1 PartsConsumed) : ICommand;

    private sealed class CommandHandler(IDocumentSession session) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var status = await session.Events.AggregateStreamAsync<ProductionOrderStatusAggregate>(request.PartsConsumed.ProductionOrderId, token: cancellationToken);
            if (status == null)
                return CommandResponse.NotFound_404().Build();

            status.Apply(request.PartsConsumed);

            session.Events.StartStream(status.ProductionOrderId, request.PartsConsumed);
            await session.SaveChangesAsync(cancellationToken);

            return CommandResponse.Accepted_202().Build();
        }
    }
}