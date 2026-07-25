using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Library.RabbitMQ.Consumer;
using Mes.Shared.Contracts.ProductionData.MaterialsAndParts;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Requests;

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
                return CommandResponse.NotFound_404().Build();

            status.Apply(request.MaterialConsumed);

            session.Events.StartStream(status.ProductionOrderId, request.MaterialConsumed);
            await session.SaveChangesAsync(cancellationToken);

            return CommandResponse.Accepted_202().Build();
        }
    }
}