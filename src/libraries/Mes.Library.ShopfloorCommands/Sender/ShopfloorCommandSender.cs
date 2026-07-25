using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Mes.Library.ShopfloorCommands.Sender;

internal sealed class ShopfloorCommandSender(
    ILogger<ShopfloorCommandSender> logger,
    IHubContext<ShopfloorCommandHubV1> hubV1Context) : IShopfloorCommandSender
{
    public async Task<ShopfloorCommandResponse> SendAsync(IShopfloorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await hubV1Context.Clients.All.SendAsync(
                nameof(ShopfloorCommandHubV1.SendCommandV1),
                command.ShopfloorKey,
                command,
                cancellationToken);

            return ShopfloorCommandResponse.Success;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Sending a shopfloor command threw an exception.");
            return ShopfloorCommandResponse.Failure;
        }
    }

    public async Task<ShopfloorCommandResponse> BroadcastAsync(IShopfloorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await hubV1Context.Clients.All.SendAsync(
                nameof(ShopfloorCommandHubV1.BroadcastCommandV1),
                command,
                cancellationToken);

            return ShopfloorCommandResponse.Success;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Broadcasting a shopfloor command threw an exception.");
            return ShopfloorCommandResponse.Failure;
        }
    }
}