using Mes.Library.ShopfloorCommands.Connection;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace Mes.Library.ShopfloorCommands.Sender;

/// <summary>
/// Default implementation of <see cref="IShopfloorCommandSender"/> that sends
/// shopfloor-to-shopfloor commands through the command hub.
/// </summary>
/// <remarks>
/// This class is internal and is automatically registered with the DI container when
/// <see cref="SenderServiceCollectionExtensions.AddShopfloorCommandSender"/> is called.
/// <para>
/// The sender uses a lazy initialization pattern for the SignalR connection, creating
/// it on first use and reusing it for subsequent send operations.
/// </para>
/// <para>
/// Commands are sent by invoking the Forward method on the hub, which publishes the
/// command to the RabbitMQ message bus. The command must implement
/// <see cref="IShopfloorToShopfloorCommand"/> and have both sender and receiver
/// shopfloor keys set.
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandSender"/>
/// <seealso cref="IShopfloorCommandHubConnectionProvider"/>
/// <seealso cref="IShopfloorToShopfloorCommand"/>
internal sealed class ShopfloorCommandSender(
    ILogger<ShopfloorCommandSender> logger,
    IShopfloorCommandHubConnectionProvider connectionProvider) : IShopfloorCommandSender
{
    /// <summary>
    /// Cached SignalR hub connection. Created lazily on first use.
    /// </summary>
    private HubConnection? _connection;

    /// <summary>
    /// Asynchronously sends a shopfloor-to-shopfloor command through the command hub.
    /// <para>
    /// This method:
    /// <list type="number">
    /// <item><description>Gets or creates the SignalR hub connection</description></item>
    /// <item><description>Invokes the Forward method on the hub with the command</description></item>
    /// <item><description>Returns Success if the operation completes without error</description></item>
    /// <item><description>Returns Failure if an exception occurs, logging the error</description></item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="command">The command to send. Must implement <see cref="IShopfloorToShopfloorCommand"/>.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result indicates whether
    /// the command was successfully sent (<see cref="ShopfloorCommandResponse.Success"/>) or
    /// if an error occurred (<see cref="ShopfloorCommandResponse.Failure"/>).
    /// </returns>
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

    /// <summary>
    /// Gets or creates the SignalR hub connection.
    /// <para>
    /// This method uses lazy initialization to create the connection on first use,
    /// and caches it for subsequent calls.
    /// </para>
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the connection operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the hub connection.</returns>
    private async Task<HubConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        return _connection ??= await connectionProvider.GetAsync("hub", cancellationToken);
    }
}