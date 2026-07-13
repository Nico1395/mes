using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Requests;

internal static class HandleProductionUnitStateChangedV1
{
    private sealed class StatusStateChangedConsumer(IMediator mediator) : IConsumer<ProductionUnitStateChangedV1>
    {
        public async Task<ConsumerResult> HandleAsync(ProductionUnitStateChangedV1 message, CancellationToken cancellationToken)
        {
            var command = new Command(message);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.IsSuccess_2xx() ? ConsumerResult.Ack() : ConsumerResult.Nack();
        }
    }

    private sealed record Command(ProductionUnitStateChangedV1 StateChanged) : ICommand;

    private sealed class CommandHandler(IDocumentSession session) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            // Validation of whether a production unit exists is done when projecting to statuses.
            // For now however, we are going to remember that event by storing it.

            session.Events.StartStream(request.StateChanged.ProductionUnitId, request.StateChanged);
            await session.SaveChangesAsync(cancellationToken);

            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}