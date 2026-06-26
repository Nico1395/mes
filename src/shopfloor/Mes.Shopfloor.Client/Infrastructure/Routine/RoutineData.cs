namespace Mes.Shopfloor.Client.Infrastructure.Routine;

public sealed record RoutineData(RoutineDataKey Key, object? Data, DateTime SetAt)
{
    public T? OfType<T>()
    {
        return Data is T casted ? casted : default;
    }

    public static RoutineData Create(RoutineDataKey key, object? data)
    {
        return new(key, data, DateTime.UtcNow);
    }
}