namespace Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Durational;

public static class EndedExtensions
{
    public static bool HasEnded(this IEnded ended)
    {
        return ended.EndedAt.HasValue;
    }

    public static void TouchEndedAt(this IEnded ended, DateTime pointInTime)
    {
        if (ended.HasEnded())
            return;
        
        ended.EndedAt = pointInTime;
    }

    public static void TouchEndedAt(this IEnded ended)
    {
        ended.TouchEndedAt(DateTime.UtcNow);
    }
}