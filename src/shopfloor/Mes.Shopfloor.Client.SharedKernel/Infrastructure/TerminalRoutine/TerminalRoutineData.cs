namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;

public sealed record TerminalRoutineData(DataKey Key, object? Data, DateTime SetAt)
{
    public T? OfType<T>()
    {
        return Data is T casted ? casted : default;
    }

    public static TerminalRoutineData Create(DataKey key, object? data)
    {
        return new(key, data, DateTime.UtcNow);
    }
}