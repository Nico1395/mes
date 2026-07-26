namespace Mes.Library.Domain.Abstractions.Timestamped;

public static class CreatedExtensions
{
    public static bool CreatedBefore(this ICreated created, DateTime pointInTime)
    {
        return created.CreatedAt < pointInTime;
    }

    public static bool CreatedBefore(this ICreated created, ICreated other)
    {
        return created.CreatedBefore(other.CreatedAt);
    }

    public static bool CreatedBeforeOrAt(this ICreated created, DateTime pointInTime)
    {
        return created.CreatedAt <= pointInTime;
    }

    public static bool CreatedBeforeOrAt(this ICreated created, ICreated other)
    {
        return created.CreatedBeforeOrAt(other.CreatedAt);
    }

    public static bool CreatedAfter(this ICreated created, DateTime pointInTime)
    {
        return created.CreatedAt > pointInTime;
    }

    public static bool CreatedAfter(this ICreated created, ICreated other)
    {
        return created.CreatedAfter(other.CreatedAt);
    }

    public static bool CreatedAfterOrAt(this ICreated created, DateTime pointInTime)
    {
        return created.CreatedAt >= pointInTime;
    }

    public static bool CreatedAfterOrAt(this ICreated created, ICreated other)
    {
        return created.CreatedAfterOrAt(other.CreatedAt);
    }

    public static bool CreatedAt(this ICreated created, DateTime pointInTime)
    {
        return created.CreatedAt == pointInTime;
    }

    public static bool CreatedAt(this ICreated created, ICreated other)
    {
        return created.CreatedAt(other.CreatedAt);
    }

    public static void TouchCreatedAt(this ICreated created, DateTime pointInTime)
    {
        created.CreatedAt = pointInTime;
    }

    public static void TouchCreatedAt(this ICreated created)
    {
        created.TouchCreatedAt(DateTime.UtcNow);
    }

    public static TimeSpan TimeSinceCreated(this ICreated created, DateTime pointInTime)
    {
        if (pointInTime < created.CreatedAt)
            return TimeSpan.Zero;

        return pointInTime - created.CreatedAt;
    }

    public static TimeSpan TimeSinceCreated(this ICreated created)
    {
        return created.TimeSinceCreated(DateTime.UtcNow);
    }
}