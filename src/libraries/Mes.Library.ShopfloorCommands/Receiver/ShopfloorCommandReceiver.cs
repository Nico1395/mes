using System.Collections.Concurrent;
using System.Reflection;
using Mes.Library.ShopfloorCommands.Connection;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mes.Library.ShopfloorCommands.Receiver;

/// <summary>
/// Default implementation of <see cref="IShopfloorCommandReceiver"/> that receives
/// and processes shopfloor commands from the command hub.
/// <para>
/// This receiver establishes a SignalR connection to the command hub and sets up
/// handlers for incoming commands, routing them to appropriate command handlers based
/// on the command type.
/// </para>
/// </summary>
/// <remarks>
/// This class is internal and is automatically registered with the DI container when
/// <see cref="ReceiverServiceCollectionExtensions.AddShopfloorCommandReceiver"/> is called.
/// <para>
/// The receiver uses a lazy initialization pattern for the SignalR connection, creating
/// it on first use and reusing it for subsequent operations. It maintains a cache of
/// command handler metadata to improve performance when processing multiple commands
/// of the same type.
/// </para>
/// <para>
/// Command routing logic:
/// <list type="number">
/// <item><description>Attempts to find a specific handler for the command type (implementing <see cref="IShopfloorCommandHandler{TCommand}"/>)</description></item>
/// <item><description>If a specific handler is found, it is invoked</description></item>
/// <item><description>If no specific handler is found, the universal handler (implementing <see cref="IShopfloorCommandHandler"/>) is used</description></item>
/// </list>
/// </para>
/// </remarks>
/// <seealso cref="IShopfloorCommandReceiver"/>
/// <seealso cref="IShopfloorCommandHandler"/>
/// <seealso cref="IShopfloorCommandHandler{TCommand}"/>
internal sealed class ShopfloorCommandReceiver(
    IServiceProvider serviceProvider,
    ILogger<ShopfloorCommandReceiver> logger,
    IShopfloorCommandHubConnectionProvider connectionProvider) : IShopfloorCommandReceiver
{
    /// <summary>
    /// Thread-safe cache of command handler metadata, mapping command types to their
    /// handler type and HandleAsync method info.
    /// </summary>
    private readonly ConcurrentDictionary<Type, (Type HandlerType, MethodInfo HandleAsync)> _commandHandlerTypes = [];

    /// <summary>
    /// Cached SignalR hub connection. Created lazily on first use.
    /// </summary>
    private HubConnection? _connection;

    /// <summary>
    /// Asynchronously starts receiving commands from the shopfloor command hub.
    /// <para>
    /// This method:
    /// <list type="number">
    /// <item><description>Gets or creates the SignalR hub connection</description></item>
    /// <item><description>Registers a handler for the ReceiveCommandV1 method</description></item>
    /// <item><description>Sets up command routing logic that uses specific handlers when available, or falls back to the universal handler</description></item>
    /// </list>
    /// </para>
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous start operation.</returns>
    public async Task StartReceivingAsync(CancellationToken cancellationToken)
    {
        var connection = await GetConnectionAsync(cancellationToken);
        connection.On<IShopfloorCommand>(ShopfloorCommandConstants.V1.Receiver.ReceiveCommand, command =>
        {
            try
            {
                Task task;

                var metadata = GetHandlerMetadataForCommand(command);
                var specificHandler = serviceProvider.GetService(metadata.HandlerType);
                if (specificHandler != null)
                {
                    task = metadata.HandleAsync.Invoke(specificHandler, [command, cancellationToken]) as Task ?? throw new InvalidOperationException();
                }
                else
                {
                    var universalHandler = serviceProvider.GetRequiredService<IShopfloorCommandHandler>();
                    task = universalHandler.HandleAsync(command, cancellationToken);
                }

                return task;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An exception occurred while receiving a shopfloor command.");
                return Task.CompletedTask;
            }
        });
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

    /// <summary>
    /// Gets the handler metadata (handler type and HandleAsync method) for a command type.
    /// <para>
    /// This method uses a cache to avoid repeated reflection operations for the same
    /// command types. It constructs the generic handler type from the command type and
    /// retrieves the HandleAsync method.
    /// </para>
    /// </summary>
    /// <param name="command">The command whose handler metadata to retrieve.</param>
    /// <returns>A tuple containing the handler type and the HandleAsync method info.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the HandleAsync method cannot be found on the handler type.
    /// </exception>
    private (Type HandlerType, MethodInfo HandleAsync) GetHandlerMetadataForCommand(IShopfloorCommand command)
    {
        return _commandHandlerTypes.GetOrAdd(command.GetType(), type =>
        {
            var handlerType = typeof(IShopfloorCommandHandler<>).MakeGenericType(type);
            var handleAsync = handlerType.GetMethod(nameof(IShopfloorCommandHandler<>.HandleAsync)) ?? throw new InvalidOperationException();

            return (handlerType, handleAsync);
        });
    }
}