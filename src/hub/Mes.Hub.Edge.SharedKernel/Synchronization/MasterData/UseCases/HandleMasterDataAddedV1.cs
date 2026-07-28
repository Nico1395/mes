using DandyMediator;
using DandyMediator.Commands;
using DandyMediator.Responses;
using Mes.Hub.Edge.SharedKernel.Synchronization.MasterData.Infrastructure;
using Mes.Library.RabbitMQ.Consumer;
using Mes.Library.SignalR;
using Mes.Library.SignalR.Connections;
using Mes.Shared.Contracts.SharedKernel.MasterData.Events;
using Microsoft.AspNetCore.SignalR;

namespace Mes.Hub.Edge.SharedKernel.Synchronization.MasterData.UseCases;

internal static class HandleMasterDataAddedV1
{
    private sealed class Consumer(IMediator mediator) : IConsumer<MasterDataAddedV1>
    {
        public async Task<ConsumerResult> HandleAsync(MasterDataAddedV1 message, CancellationToken cancellationToken)
        {
            var command = new Command(message);
            var response = await mediator.SendAsync(command, cancellationToken);

            return response.IsAccepted_202() ? ConsumerResult.Ack() : ConsumerResult.NackRequeue();
        }
    }

    private sealed record Command(MasterDataAddedV1 Added) : ICommand;

    private sealed class CommandHandler(
        ISignalRConnectionManager connectionManager,
        IHubContext<MasterDataHub> masterDataHub) : ICommandHandler<Command>
    {
        public async Task<ICommandResponse> HandleAsync(Command request, CancellationToken cancellationToken)
        {
            await masterDataHub.BroadcastOrInvokeAsync(
                connectionManager,
                MasterDataHub.KeyPrefix,
                request.Added.ShopfloorKeys,
                MasterDataPushConstants.V1.Shopfloor.MasterDataAddedV1,
                request.Added,
                cancellationToken);

            return CommandResponse.Accepted_202().Build();
        }
    }
}