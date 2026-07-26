using StackExchange.Redis;

namespace Mes.Library.SignalR.Connections;

internal sealed class SignalRConnectionManager(IConnectionMultiplexer redis) : ISignalRConnectionManager
{
    private readonly IDatabase _database = redis.GetDatabase();

    public async Task<string[]> GetConnectionIdsAsync(string prefix, string key, CancellationToken cancellationToken)
    {
        var key2Connection = CreateKey2Connection(prefix, key);
        var connectionIds = await _database.SetMembersAsync(key2Connection);
        return connectionIds.Select(v => v.ToString()).ToArray();
    }

    public async Task<string?> GetKeyForConnectionIdAsync(string prefix, string connectionId, CancellationToken cancellationToken)
    {
        var connection2Key = CreateConnection2Key(prefix, connectionId);
        return await _database.StringGetAsync(connection2Key);
    }

    public async Task AddConnectionIdAsync(string prefix, string key, string connectionId, CancellationToken cancellationToken)
    {
        var key2Connection = CreateKey2Connection(prefix, key);
        await _database.SetAddAsync(key2Connection, connectionId);

        var connection2Key = CreateKey2Connection(prefix, connectionId);
        await _database.StringSetAsync(connection2Key, key2Connection);
    }

    public async Task DeleteConnectionIdAsync(string prefix, string connectionId, CancellationToken cancellationToken)
    {
        var key = await GetKeyForConnectionIdAsync(prefix, connectionId, cancellationToken);
        if (string.IsNullOrWhiteSpace(key))
            return;

        var shopfloorToConnectionKey = CreateKey2Connection(prefix, key);
        await _database.SetRemoveAsync(shopfloorToConnectionKey, connectionId);

        var connectionKeyRedis = CreateConnection2Key(prefix, connectionId);
        await _database.KeyDeleteAsync(connectionKeyRedis);
    }

    private static string CreateKey2Connection(string prefix, string key)
    {
        return $"{prefix.Trim(':')}:key-2-connection:{key}";
    }

    private static string CreateConnection2Key(string prefix, string connectionId)
    {
        return $"{prefix.Trim(':')}:connection-2-key:{connectionId}";
    }
}