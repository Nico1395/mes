namespace Mes.Shopfloor.Client.SharedKernel.Infrastructure.TerminalRoutine;

public interface ITerminalRoutineContext
{
    // I dont like the 'internal set', so thats something I would prefer to be solved differently for sure.
    DateTime CurrentIterationStartedAt { get; internal set; }
    DateTime LastIterationCompletedAt { get; internal set; }
    IReadOnlyDictionary<DataKey, TerminalRoutineData> Data { get; }
    void SetData(TerminalRoutineData data);
    TerminalRoutineData? GetData(DataKey key);
}