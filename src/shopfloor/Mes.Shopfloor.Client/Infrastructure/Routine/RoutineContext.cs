using System.Collections.Concurrent;

namespace Mes.Shopfloor.Client.Infrastructure.Routine;

internal sealed class RoutineContext : IRoutineContext
{
    private readonly ConcurrentDictionary<RoutineDataKey, RoutineData> _data = [];

    public DateTime CurrentIterationStartedAt { get; set; }
    public DateTime LastIterationCompletedAt { get; set; }
    public IReadOnlyDictionary<RoutineDataKey, RoutineData> Data => _data;

    public void SetData(RoutineData data)
    {
        _data[data.Key] = data;
    }

    public RoutineData? GetData(RoutineDataKey key)
    {
        return _data.GetValueOrDefault(key);
    }
}