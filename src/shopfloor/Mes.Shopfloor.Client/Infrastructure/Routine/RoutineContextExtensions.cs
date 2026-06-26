using System.Diagnostics.CodeAnalysis;

namespace Mes.Shopfloor.Client.Infrastructure.Routine;

internal static class RoutineContextExtensions
{
    public static void Set(this IRoutineContext context, RoutineDataKey key, object? data)
    {
        context.SetData(RoutineData.Create(key, data));
    }

    public static bool SetIfNotNull(this IRoutineContext context, RoutineDataKey key, [NotNullWhen(true)] object? data)
    {
        if (data == null)
            return false;
        
        context.Set(key, data);
        return true;
    }

    public static object? Get(this IRoutineContext context, RoutineDataKey key)
    {
        return context.GetData(key)?.Data;
    }

    public static T? Get<T>(this IRoutineContext context, RoutineDataKey key)
    {
        var data = context.GetData(key);
        return data == null ? default : data.OfType<T>();
    }

    public static object? GetRequired(this IRoutineContext context, RoutineDataKey key)
    {
        return context.Get(key) ?? throw new RequiredRoutineDataMissingException(key);
    }

    public static T GetRequired<T>(this IRoutineContext context, RoutineDataKey key)
    {
        return context.Get<T>(key) ?? throw new RequiredRoutineDataMissingException(key);
    }

    public static bool IsEvenSecond(this IRoutineContext context)
    {
        return context.CurrentIterationStartedAt.Second % 2 == 0;
    }

    public static bool IsOddSecond(this IRoutineContext context)
    {
        return context.CurrentIterationStartedAt.Second % 2 != 0;
    }

    public static bool InCurrentIteration(this IRoutineContext context, DateTime pointInTime)
    {
        return context.CurrentIterationStartedAt <= pointInTime;
    }

    public static bool InPreviousIteration(this IRoutineContext context, DateTime pointInTime)
    {
        return context.CurrentIterationStartedAt > pointInTime;
    }
}