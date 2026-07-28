namespace Mes.Shared.Contracts.SharedKernel.Abstractions.Durational;

public static class DurationalExtensions
{
    public static TimeSpan? GetDuration(this IDurational durational)
    {
        if (!durational.EndedAt.HasValue)
            return null;

        return durational.EndedAt.Value - durational.StartedAt;
    }

    public static TimeSpan GetDurationUntilEndOrCurrent(this IDurational durational)
    {
        var upper = durational.EndedAt ?? DateTime.UtcNow;
        return upper - durational.StartedAt;
    }

    public static bool IsValid(this IDurational durational)
    {
        if (!durational.EndedAt.HasValue)
            return true;

        return durational.EndedAt.Value >= durational.StartedAt;
    }

    public static IOrderedEnumerable<IDurational> OrderBySuccession(this IEnumerable<IDurational> durationals)
    {
        return durationals.OrderBy(d => d.StartedAt);
    }

    public static bool HasCohesiveOrder(this IEnumerable<IDurational> durationals)
    {
        IDurational? previous = null;
        foreach (var current in durationals)
        {
            if (previous != null && !current.StartedAfter(previous))
                return false;

            previous = current;
        }

        return true;
    }

    public static TimeSpan? GetTotalDuration(this IEnumerable<IDurational> durationals)
    {
        if (!durationals.HasCohesiveOrder())
            return null;

        return durationals.Aggregate(
            TimeSpan.Zero,
            (current, durational) => current + durational.GetDurationUntilEndOrCurrent()
        );
    }

    public static IEnumerable<TDurational> WhereBetween<TDurational>(this IEnumerable<TDurational> source, DateTime start, DateTime end)
        where TDurational : IDurational
    {
        return source.Where(item =>
            item.StartedAt >= start &&
            item.EndedAt.HasValue &&
            item.EndedAt.Value <= end);
    }

    public static IEnumerable<TDurational> WhereBetween<TDurational>(this IEnumerable<TDurational> source, DateTime start, DateTime end, Func<TDurational, bool> predicate)
        where TDurational : IDurational
    {
        return source.Where(item =>
            item.StartedAt >= start &&
            item.EndedAt.HasValue &&
            item.EndedAt.Value <= end &&
            predicate(item));
    }

    public static IEnumerable<TDurational> WhereBetweenAndOn<TDurational>(this IEnumerable<TDurational> source, DateTime start, DateTime end)
        where TDurational : IDurational
    {
        return source.Where(item =>
            item.StartedAt <= end &&
            (item.EndedAt == null || item.EndedAt >= start));
    }

    public static IEnumerable<TDurational> WhereBetweenAndOn<TDurational>(this IEnumerable<TDurational> source, DateTime start, DateTime end, Func<TDurational, bool> predicate)
        where TDurational : IDurational
    {
        return source.Where(item =>
            item.StartedAt <= end &&
            (item.EndedAt == null || item.EndedAt >= start) &&
            predicate(item));
    }

    public static IEnumerable<TResult> SelectBetween<TDurational, TResult>(this IEnumerable<TDurational> source, DateTime start, DateTime end, Func<TDurational, TResult> selector)
        where TDurational : IDurational
    {
        return source.WhereBetween(start, end).Select(selector);
    }

    public static IEnumerable<TResult> SelectBetweenAndOn<TDurational, TResult>(this IEnumerable<TDurational> source, DateTime start, DateTime end, Func<TDurational, TResult> selector)
        where TDurational : IDurational
    {
        return source.WhereBetweenAndOn(start, end).Select(selector);
    }
}