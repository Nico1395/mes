namespace Mes.Library.SignalR.Connections;

public interface ISignalRConnectionManager
{
    Task<string[]> GetConnectionIdsAsync(string prefix, string key, CancellationToken cancellationToken);
    Task<string?> GetKeyForConnectionIdAsync(string prefix, string connectionId, CancellationToken cancellationToken);
    Task AddConnectionIdAsync(string prefix, string key, string connectionId, CancellationToken cancellationToken);
    Task DeleteConnectionIdAsync(string prefix, string connectionId, CancellationToken cancellationToken);
}