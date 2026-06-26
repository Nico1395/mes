namespace Mes.Shopfloor.Client.Infrastructure.Routine;

public interface IRoutineContext
{
    // I dont like the 'internal set', so thats something I would prefer to be solved differently for sure.
    DateTime CurrentIterationStartedAt { get; internal set; }
    DateTime LastIterationCompletedAt { get; internal set; }
    IReadOnlyDictionary<RoutineDataKey, RoutineData> Data { get; }
    void SetData(RoutineData data);
    RoutineData? GetData(RoutineDataKey key);
}