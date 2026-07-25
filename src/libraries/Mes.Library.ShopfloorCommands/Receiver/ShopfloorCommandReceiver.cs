using System.Collections.Concurrent;
using System.Reflection;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Mes.Library.ShopfloorCommands.Receiver;

internal sealed class ShopfloorCommandReceiver(
    IServiceProvider serviceProvider,
    ILogger<ShopfloorCommandReceiver> logger,
    IShopfloorCommandHubConnectionFactory connectionFactory) : IShopfloorCommandReceiver, IAsyncDisposable
{
    private readonly ConcurrentDictionary<Type, (Type HandlerType, MethodInfo HandleAsync)> _commandHandlerTypes = [];
    private HubConnection? _connection;

    public async ValueTask DisposeAsync()
    {
        await StopReceivingAsync(CancellationToken.None);
    }

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

    public async Task StopReceivingAsync(CancellationToken cancellationToken)
    {
        if (_connection != null)
            await _connection.DisposeAsync();

        _connection = null;
    }

    private async Task<HubConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        return _connection ??= await connectionFactory.CreateV1Async(cancellationToken);
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