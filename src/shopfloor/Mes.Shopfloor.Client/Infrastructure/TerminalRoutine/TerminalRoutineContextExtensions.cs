using System.Diagnostics.CodeAnalysis;

namespace Mes.Shopfloor.Client.Infrastructure.TerminalRoutine;

public static class TerminalRoutineContextExtensions
{
    public static void Set(this ITerminalRoutineContext context, DataKey key, object? data)
    {
        context.SetData(TerminalRoutineData.Create(key, data));
    }

    public static bool SetIfNotNull(this ITerminalRoutineContext context, DataKey key, [NotNullWhen(true)] object? data)
    {
        if (data == null)
            return false;
        
        context.Set(key, data);
        return true;
    }

    public static object? Get(this ITerminalRoutineContext context, DataKey key)
    {
        return context.GetData(key)?.Data;
    }

    public static T? Get<T>(this ITerminalRoutineContext context, DataKey key)
    {
        var data = context.GetData(key);
        return data == null ? default : data.OfType<T>();
    }

    public static object? GetRequired(this ITerminalRoutineContext context, DataKey key)
    {
        return context.Get(key) ?? throw new RequiredTerminalRoutineDataMissingException(key);
    }

    public static T GetRequired<T>(this ITerminalRoutineContext context, DataKey key)
    {
        return context.Get<T>(key) ?? throw new RequiredTerminalRoutineDataMissingException(key);
    }

    public static bool IsEvenSecond(this ITerminalRoutineContext context)
    {
        return context.CurrentIterationStartedAt.Second % 2 == 0;
    }

    public static bool IsOddSecond(this ITerminalRoutineContext context)
    {
        return context.CurrentIterationStartedAt.Second % 2 != 0;
    }

    public static bool InCurrentIteration(this ITerminalRoutineContext context, DateTime pointInTime)
    {
        return context.CurrentIterationStartedAt <= pointInTime;
    }

    public static bool InPreviousIteration(this ITerminalRoutineContext context, DateTime pointInTime)
    {
        return context.CurrentIterationStartedAt > pointInTime;
    }
}