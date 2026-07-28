namespace Mes.Hub.Edge.Synchronization.MasterData.Infrastructure;

internal sealed class MasterDataHub : Microsoft.AspNetCore.SignalR.Hub
{
    public const string KeyPrefix = "edge:signalr:sync:master-data";
}