using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Mes.Hub.Edge.Synchronization.MasterData.Infrastructure;
using Mes.Library.RabbitMQ.Consumer;
using Mes.Library.SignalR;
using Mes.Library.SignalR.Connections;
using Mes.Shared.Contracts.SharedKernel.MasterData.Events;
using Microsoft.AspNetCore.SignalR;

namespace Mes.Hub.Edge.Synchronization.MasterData.UseCases;

internal static class HandleMasterDataUpdatedV1
{
    private sealed class Consumer(IMediator mediator) : IConsumer<MasterDataUpdatedV1>
    {
        public async Task<ConsumerResult> HandleAsync(MasterDataUpdatedV1 message, CancellationToken cancellationToken)
        {
            var command = new Command(message);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.IsAccepted_202() ? ConsumerResult.Ack() : ConsumerResult.NackRequeue();
        }
    }

    private sealed record Command(MasterDataUpdatedV1 Updated) : ICommand;

    private sealed class CommandHandler(
        ISignalRConnectionManager connectionManager,
        IHubContext<MasterDataHub> masterDataHub) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            await masterDataHub.BroadcastOrInvokeAsync(
                connectionManager,
                MasterDataHub.KeyPrefix,
                request.Updated.ShopfloorKeys,
                MasterDataPushConstants.V1.Shopfloor.MasterDataUpdatedV1,
                request.Updated,
                cancellationToken);

            return CommandResponse.Accepted_202().Build();
        }
    }
}