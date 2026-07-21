using System.Numerics;

namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Scheduled;

public static class ScheduledExtensions
{
    public static IEnumerable<TScheduled> Where<TScheduled>(this TScheduled scheduled, Func<TScheduled, bool> predicate)
        where TScheduled : class, IScheduled<TScheduled>
    {
        if (predicate(scheduled))
            yield return scheduled;

        foreach (var match in scheduled.Children.SelectMany(child => child.Where(predicate)))
            yield return match;
    }

    public static TScheduled? FirstOrDefault<TScheduled>(this TScheduled scheduled, Func<TScheduled, bool> predicate)
        where TScheduled : class, IScheduled<TScheduled>
    {
        return scheduled.Where(predicate).FirstOrDefault();
    }
    
    public static TScheduled First<TScheduled>(this TScheduled scheduled, Func<TScheduled, bool> predicate)
        where TScheduled : class, IScheduled<TScheduled>
    {
        return scheduled.Where(predicate).First();
    }

    public static TScheduled? LastOrDefault<TScheduled>(this TScheduled scheduled, Func<TScheduled, bool> predicate)
        where TScheduled : class, IScheduled<TScheduled>
    {
        return scheduled.Where(predicate).LastOrDefault();
    }
    
    public static TScheduled Last<TScheduled>(this TScheduled scheduled, Func<TScheduled, bool> predicate)
        where TScheduled : class, IScheduled<TScheduled>
    {
        return scheduled.Where(predicate).Last();
    }

    public static TScheduled? SingleOrDefault<TScheduled>(this TScheduled scheduled, Func<TScheduled, bool> predicate)
        where TScheduled : class, IScheduled<TScheduled>
    {
        return scheduled.Where(predicate).SingleOrDefault();
    }
    
    public static TScheduled Single<TScheduled>(this TScheduled scheduled, Func<TScheduled, bool> predicate)
        where TScheduled : class, IScheduled<TScheduled>
    {
        return scheduled.Where(predicate).Single();
    }

    public static bool Any<TScheduled>(this TScheduled scheduled, Func<TScheduled, bool> predicate)
        where TScheduled : class, IScheduled<TScheduled>
    {
        return scheduled.Where(predicate).Any();
    }

    public static TValue Sum<TScheduled, TValue>(this TScheduled scheduled, Func<TScheduled, TValue> selector)
        where TScheduled : class, IScheduled<TScheduled>
        where TValue : INumber<TValue>
    {
        var sum = selector(scheduled);
        return scheduled.Children.Aggregate(sum, (current, child) => current + child.Sum(selector));
    }

    public static TScheduled? ToScheduled<TScheduled>(this List<TScheduled> nodes)
        where TScheduled : class, IScheduled<TScheduled>
    {
        var potentialRoots = nodes.Where(i => !i.ParentId.HasValue).ToList();
        if (potentialRoots.Count != 1)
            return null;

        var root = potentialRoots[0];
        return ToSchedule(nodes, root);
    }

    private static TScheduled ToSchedule<TScheduled>(List<TScheduled> nodes, TScheduled node)
        where TScheduled : class, IScheduled<TScheduled>
    {
        var children = nodes.Where(n => n.ParentId == node.Id).Select(child => ToSchedule(nodes, child));

        node.Children.Clear();
        node.Children.AddRange(children);

        return node;
    }
}