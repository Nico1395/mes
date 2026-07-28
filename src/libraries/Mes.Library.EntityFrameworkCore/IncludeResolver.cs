using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Mes.Library.EntityFrameworkCore;

public static class IncludeResolver
{
    private static readonly ConcurrentDictionary<Type, string[]> _includes = new();

    internal static void CalculateIncludeStrings(DbContext context)
    {
        foreach (var entityType in context.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            var paths = CollectIncludePaths(entityType, context, "", []).ToArray();

            _includes[clrType] = paths;
        }
    }

    public static IQueryable<TEntity> IncludeRecursively<TEntity>(this IQueryable<TEntity> query)
        where TEntity : class
    {
        if (!_includes.TryGetValue(typeof(TEntity), out var includePaths))
            return query;

        foreach (var path in includePaths)
            query = query.Include(path);

        return query;
    }

    private static List<string> CollectIncludePaths(
        IEntityType entityType,
        DbContext context,
        string currentPath,
        HashSet<Type> visitedTypes)
    {
        var paths = new List<string>();
        var clrType = entityType.ClrType;

        if (!visitedTypes.Add(clrType))
            return paths;

        foreach (var navigation in entityType.GetNavigations())
        {
            var newPath = string.IsNullOrEmpty(currentPath) ? navigation.Name : $"{currentPath}.{navigation.Name}";
            paths.Add(newPath);

            var targetEntityType = navigation.TargetEntityType.ClrType;
            var targetClrType = context.Model.FindEntityType(targetEntityType)?.ClrType;
            if (targetClrType == null)
                continue;

            var targetEntityTypeFromModel = context.Model.FindEntityType(targetClrType);
            if (targetEntityTypeFromModel == null)
                continue;

            paths.AddRange(CollectIncludePaths(
                targetEntityTypeFromModel,
                context,
                newPath,
                [.. visitedTypes]));
        }

        return paths;
    }
}