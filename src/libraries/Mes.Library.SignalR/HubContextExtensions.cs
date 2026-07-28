using Mes.Library.SignalR.Connections;
using Microsoft.AspNetCore.SignalR;

namespace Mes.Library.SignalR;

public static class HubContextExtensions
{
    public static async Task BroadcastOrInvokeAsync<THub>(
        this IHubContext<THub> hubContext,
        ISignalRConnectionManager connectionManager,
        string keyPrefix,
        string[]? shopfloorKeys,
        string methodName,
        object message,
        CancellationToken cancellationToken)
        where THub : Hub
    {
        if (shopfloorKeys is { Length: > 0 })
            await InvokeAsync(hubContext, connectionManager, keyPrefix, methodName, shopfloorKeys, message, cancellationToken);
        else
            await BroadcastAsync(hubContext, methodName, message, cancellationToken);
    }

    private static async Task InvokeAsync<THub>(IHubContext<THub> hubContext, ISignalRConnectionManager connectionManager, string keyPrefix, string methodName, string[] shopfloorKeys, object message, CancellationToken cancellationToken)
        where THub : Hub
    {
        var connectionIds = new List<string>();
        foreach (var shopfloorKey in shopfloorKeys)
        {
            var shopfloorConnectionIds = await connectionManager.GetConnectionIdsAsync(
                keyPrefix,
                shopfloorKey,
                cancellationToken);

            if (shopfloorConnectionIds.Length > 0)
                connectionIds.Add(shopfloorConnectionIds[0]);
        }

        await hubContext.Clients.Clients(connectionIds).SendAsync(
            methodName,
            message,
            cancellationToken);
    }

    private static async Task BroadcastAsync<THub>(IHubContext<THub> hubContext, string methodName, object message, CancellationToken cancellationToken)
        where THub : Hub
    {
        await hubContext.Clients.All.SendAsync(
            methodName,
            message,
            cancellationToken);
    }
}