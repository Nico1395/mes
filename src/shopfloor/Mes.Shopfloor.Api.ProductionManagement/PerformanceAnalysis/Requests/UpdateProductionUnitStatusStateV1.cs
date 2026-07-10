using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Factories;
using Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Application;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Mes.Shopfloor.Shared.SharedKernel.Messaging.Consumer;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Requests;

internal static class UpdateProductionUnitStatusStateV1
{
    private sealed class StatusStateChangedConsumer(IMediator mediator) : IConsumer<ProdUnitStateChangedEvent>
    {
        public async Task<ConsumerResult> HandleAsync(ProdUnitStateChangedEvent message, CancellationToken cancellationToken)
        {
            var command = new Command(message.ProductionUnitId, message.OldStateId, message.NewStateId, message.OccurredAtUtc);
            var response = await mediator.SendAsync(command, cancellationToken);

            return !response.IsSuccess_2xx() ? ConsumerResult.Nack() : ConsumerResult.Ack();
        }
    }

    private sealed record Command(
        Guid ProductionUnitId,
        Guid OldStateId,
        Guid NewStateId,
        DateTime OccurredAtUtc) : ICommand;
    
    private sealed class CommandHandler(
        DbContext context,
        IProductionUnitStatusFactory factory) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            // Dismiss event if the production unit doesn't exist.
            var productionUnitExists = await context.ProductionUnitExistsAsync(request.ProductionUnitId, cancellationToken);
            if (!productionUnitExists)
                return CommandResponseFactory.BadRequest_400().Build();

            // Dismiss event if the target state doesn't exist.
            var state = await context.GetProductionUnitStateByIdAsync(request.ProductionUnitId, cancellationToken);
            if (state == null)
                return CommandResponseFactory.BadRequest_400().Build();

            // Map or create a new status. If both fail, the status could not be created.
            var status = await context.GetStatusByProductionUnitIdEagerAsync(request.ProductionUnitId, cancellationToken);
            status ??= await factory.CreateAsync(request.ProductionUnitId, cancellationToken);
            if (status == null)
                return CommandResponseFactory.BadRequest_400().Build();

            // Set the new state and save.
            var statusState = ProductionUnitStatusState.FromState(status.ProductionUnitId, state, request.OccurredAtUtc);
            status.SetState(statusState);
            context.Add(status);
            await context.SaveChangesAsync(cancellationToken);

            return CommandResponseFactory.Accepted_202().Build();
        }
    }
}