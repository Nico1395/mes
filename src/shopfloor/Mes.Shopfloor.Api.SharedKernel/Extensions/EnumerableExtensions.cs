namespace Mes.Shopfloor.Api.SharedKernel.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
    {
        return source.Where(item => item != null).Cast<T>();
    }

    public static List<T> CastToList<T>(this IEnumerable<object> source)
    {
        return source.Cast<T>().ToList();
    }

    public static List<T> ToListOfType<T>(this IEnumerable<object> source)
    {
        return source.OfType<T>().ToList();
    }
}