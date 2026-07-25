using Microsoft.AspNetCore.SignalR.Client;

namespace Mes.Library.ShopfloorCommands.Connection;

public interface IShopfloorCommandHubConnectionProvider
{
    Task<HubConnection> GetAsync(string key, CancellationToken cancellationToken);
    void Remove(string key);
}