using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Mes.Library.RabbitMQ.Producer;
using Mes.Library.ShopfloorCommands.Connection;
using Microsoft.AspNetCore.SignalR;

namespace Mes.Library.ShopfloorCommands.Hub;

internal sealed class ShopfloorCommandHub(IMessagePublisher messagePublisher) : Microsoft.AspNetCore.SignalR.Hub
{
    private static ConcurrentDictionary<string, string> ShopfloorConnections { get; } = [];

    [HubMethodName(ShopfloorCommandConstants.V1.Hub.RegisterShopfloor)]
    public Task RegisterShopfloorV1(string shopfloorKey)
    {
        ShopfloorConnections[shopfloorKey] = Context.ConnectionId;
        return Task.CompletedTask;
    }

    [HubMethodName(ShopfloorCommandConstants.V1.Hub.SendCommand)]
    public async Task SendCommandV1(IShopfloorCommand command)
    {
        if (!TryGetConnectionId(command.ReceiverShopfloorKey, out var connectionId))
            throw new Exception($"Shopfloor mit Key {command.ReceiverShopfloorKey} nicht verbunden.");

        await Clients.Client(connectionId).SendAsync(ShopfloorCommandConstants.V1.Receiver.ReceiveCommand, command);
    }

    [HubMethodName(ShopfloorCommandConstants.V1.Hub.BroadcastCommand)]
    public async Task BroadcastCommandV1(IShopfloorCommand command)
    {
        await Clients.All.SendAsync(ShopfloorCommandConstants.V1.Receiver.ReceiveCommand, command);
    }

    [HubMethodName(ShopfloorCommandConstants.V1.Hub.Forward)]
    public Task ForwardV1(IShopfloorToShopfloorCommand command)
    {
        return messagePublisher.PublishAsync(command, Context.ConnectionAborted);
    }

    private static bool TryGetConnectionId(string shopfloorKey, [NotNullWhen(true)] out string? connectionId)
    {
        return ShopfloorConnections.TryGetValue(shopfloorKey, out connectionId);
    }
}