using Mes.Library.ShopfloorCommands.Connection;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace Mes.Library.ShopfloorCommands.Sender;

internal sealed class ShopfloorCommandSender(
    ILogger<ShopfloorCommandSender> logger,
    IShopfloorCommandHubConnectionProvider connectionProvider) : IShopfloorCommandSender
{
    private HubConnection? _connection;

    public async Task<ShopfloorCommandResponse> SendAsync(IShopfloorToShopfloorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var connection = await GetConnectionAsync(cancellationToken);
            await connection.InvokeAsync(ShopfloorCommandConstants.V1.Hub.Forward, command, cancellationToken);

            return ShopfloorCommandResponse.Success;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An exception was thrown when sending command {commandTypeName}", command.GetType().Name);
            return ShopfloorCommandResponse.Failure;
        }
    }

    private async Task<HubConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        return _connection ??= await connectionProvider.GetAsync("hub", cancellationToken);
    }
}