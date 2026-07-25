using System.Collections.Concurrent;
using System.Reflection;
using Mes.Library.ShopfloorCommands.Connection;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mes.Library.ShopfloorCommands.Receiver;

internal sealed class ShopfloorCommandReceiver(
    IServiceProvider serviceProvider,
    ILogger<ShopfloorCommandReceiver> logger,
    IShopfloorCommandHubConnectionProvider connectionProvider) : IShopfloorCommandReceiver
{
    private readonly ConcurrentDictionary<Type, (Type HandlerType, MethodInfo HandleAsync)> _commandHandlerTypes = [];
    private HubConnection? _connection;

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

    private async Task<HubConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        return _connection ??= await connectionProvider.GetAsync("hub", cancellationToken);
    }

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