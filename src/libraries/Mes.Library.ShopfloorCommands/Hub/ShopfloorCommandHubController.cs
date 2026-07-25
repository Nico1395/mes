using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Mes.Library.ShopfloorCommands.Hub;

internal sealed class ShopfloorCommandHubController(
    ILogger<ShopfloorCommandHubController> logger,
    IHubContext<ShopfloorCommandHub> hubV1Context) : IShopfloorCommandHubController
{
    public async Task<ShopfloorCommandResponse> SendAsync(IShopfloorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await hubV1Context.Clients.All.SendAsync(
                nameof(ShopfloorCommandHub.SendCommandV1),
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
                nameof(ShopfloorCommandHub.BroadcastCommandV1),
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