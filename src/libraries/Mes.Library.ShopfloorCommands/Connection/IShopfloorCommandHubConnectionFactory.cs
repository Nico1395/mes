using Microsoft.AspNetCore.SignalR.Client;

namespace Mes.Library.ShopfloorCommands.Connection;

public interface IShopfloorCommandHubConnectionFactory
{
    Task<HubConnection> CreateV1Async(CancellationToken cancellationToken);
}