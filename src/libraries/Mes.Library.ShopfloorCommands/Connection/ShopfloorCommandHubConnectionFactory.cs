using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Mes.Library.ShopfloorCommands.Connection;

internal sealed class ShopfloorCommandHubConnectionFactory(ShopfloorCommandHubConnectionReceiverConfiguration configuration) : IShopfloorCommandHubConnectionFactory
{
    public async Task<HubConnection> CreateV1Async(CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(configuration.HubBaseUrl))
            throw new InvalidOperationException("The shopfloor command hub base URL was not configured.");

        if (string.IsNullOrWhiteSpace(configuration.ShopfloorKey))
            throw new InvalidOperationException("The shopfloor key was not configured.");

        var connection = new HubConnectionBuilder()
            .AddJsonProtocol()
            .WithAutomaticReconnect()
            .WithUrl($"{configuration.HubBaseUrl}/cmd/v1/")
            .Build();

        await connection.StartAsync(cancellationToken);
        await connection.InvokeAsync(ShopfloorCommandConstants.V1.Hub.RegisterShopfloor, configuration.ShopfloorKey, cancellationToken: cancellationToken);

        return connection;
    }
}