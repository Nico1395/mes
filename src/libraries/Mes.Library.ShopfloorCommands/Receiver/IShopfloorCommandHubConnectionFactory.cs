using Microsoft.AspNetCore.SignalR.Client;

namespace Mes.Library.ShopfloorCommands.Receiver;

public interface IShopfloorCommandHubConnectionFactory
{
    Task<HubConnection> CreateV1Async(CancellationToken cancellationToken);
}