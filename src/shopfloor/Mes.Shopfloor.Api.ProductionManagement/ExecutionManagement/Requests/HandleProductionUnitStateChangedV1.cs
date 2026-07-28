using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Marten;
using Mes.Library.RabbitMQ.Consumer;
using Mes.Shared.Contracts.SharedKernel.ProductionData.Events.ProductionUnits;
using Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionUnitAnalysis.Application;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.ExecutionManagement.Requests;

internal sealed class HandleProductionUnitStateChangedV1
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

    private sealed class CommandHandler(
        IMediator mediator,
        IDocumentSession session,
        DbContext context) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            var newState = await context.GetProductionUnitStateByIdAsync(request.StateChanged.StateId, cancellationToken);
            if (newState == null)
            {
                // This is not good but for now I don't know what the system should do here.
                // The terminal could not have set the state if it wasn't queryable at startup.
                // TODO -> Maybe the terminals should rehydrate their cache every so often.
                // TODO -> Maybe the edge server could send out a message to all terminals to refresh after every sync.

                return CommandResponse.Accepted_202().Build();
            }

            if (newState.IsProductive)
            {
                // TODO -> Maybe this logic should rather be part of reacting to quantity, but if done so, we should limit of often we do that
                await mediator.SendAsync(new EvaluateProductionOrderPerformanceV1.Command(request.StateChanged.ProductionOrderId), cancellationToken);
                return CommandResponse.Accepted_202().Build();
            }

            if (newState.IsIdle)
            {
                // Maybe give the terminal another order so it has something to do?
                // - for that we need to know what orders are scheduled
                // - for that we need to pick the most important order that fits from now until the next scheduled task for that production unit
                // - for that we might need some validation setup for someone to approve that order being booked OR
                // - for this we should have some kind of configuration layer in our master data
            }

            // The state is neither productive nor idle, so its a problem we need to address.
            // - check the master data logic, whether some kind of automatic handling logic is configured
            // - otherwise publish an event that could display an issue on some dashboard after some configurable threshold, if that is planned

            var productionUnitStatus = await session.GetProductionUnitStatusByIdAsync(request.StateChanged.ProductionUnitId, cancellationToken);

            // TODO

            return CommandResponse.Accepted_202().Build();
        }
    }
}