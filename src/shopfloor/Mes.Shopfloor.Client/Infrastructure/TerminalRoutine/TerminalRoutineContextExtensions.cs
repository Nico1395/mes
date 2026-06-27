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

    public static object GetOrDefault(this ITerminalRoutineContext context, DataKey key, object defaultValue)
    {
        return context.GetData(key)?.Data ?? defaultValue;
    }

    public static T GetOrDefault<T>(this ITerminalRoutineContext context, DataKey key, T defaultValue)
    {
        var data = context.GetData(key);
        if (data == null)
            return defaultValue;
        
        return data.OfType<T>() ?? defaultValue;
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

    public static bool HasChanged(this ITerminalRoutineContext context, DataKey key, object? value)
    {
        return context.Get(key)?.Equals(value) ?? value == null;
    }

    public static bool HasChanged<T>(this ITerminalRoutineContext context, DataKey key, T? value)
    {
        return context.Get<T>(key)?.Equals(value) ?? value == null;
    }

    public static bool HasChanged<T>(this ITerminalRoutineContext context, DataKey key, T? value, Func<T?, T?, bool> predicate)
    {
        var data = context.Get<T>(key);
        return predicate(data, value);
    }
}