namespace Mes.Shared.Contracts.SharedKernel.Abstractions.Durational;

public static class DurationalDateTimeExtensions
{
    public static bool LiesIn(this DateTime pointInTime, IDurational durational)
    {
        var end = durational.EndedAt ?? DateTime.UtcNow;
        return durational.StartedAt <= pointInTime && pointInTime <= end;
    }
}