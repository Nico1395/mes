using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.SignalR;

namespace Mes.Library.ShopfloorCommands.Sender;

internal sealed class ShopfloorCommandHubV1 : Hub
{
    private static ConcurrentDictionary<string, string> ShopfloorConnections { get; } = [];

    [HubMethodName(ShopfloorCommandConstants.V1.Sender.RegisterShopfloor)]
    public Task RegisterShopfloorV1(string shopfloorKey)
    {
        ShopfloorConnections[shopfloorKey] = Context.ConnectionId;
        return Task.CompletedTask;
    }

    [HubMethodName(ShopfloorCommandConstants.V1.Sender.SendCommand)]
    public async Task SendCommandV1(string shopfloorKey, IShopfloorCommand command)
    {
        if (!TryGetConnectionId(shopfloorKey, out var connectionId))
            throw new Exception($"Shopfloor mit Key {shopfloorKey} nicht verbunden.");

        await Clients.Client(connectionId).SendAsync(ShopfloorCommandConstants.V1.Receiver.ReceiveCommand, command);
    }

    [HubMethodName(ShopfloorCommandConstants.V1.Sender.BroadcastCommand)]
    public async Task BroadcastCommandV1(IShopfloorCommand command)
    {
        await Clients.All.SendAsync(ShopfloorCommandConstants.V1.Receiver.ReceiveCommand, command);
    }

    private static bool TryGetConnectionId(string shopfloorKey, [NotNullWhen(true)] out string? connectionId)
    {
        return ShopfloorConnections.TryGetValue(shopfloorKey, out connectionId);
    }
}