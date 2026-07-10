using System.Collections.Concurrent;

namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;

internal sealed class TerminalRoutineContext : ITerminalRoutineContext
{
    private readonly ConcurrentDictionary<DataKey, TerminalRoutineData> _data = [];

    public DateTime CurrentIterationStartedAt { get; set; }
    public DateTime LastIterationCompletedAt { get; set; }
    public IReadOnlyDictionary<DataKey, TerminalRoutineData> Data => _data;

    public void SetData(TerminalRoutineData data)
    {
        _data[data.Key] = data;
    }

    public TerminalRoutineData? GetData(DataKey key)
    {
        return _data.GetValueOrDefault(key);
    }
}