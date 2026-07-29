using System.Reflection;
using Mes.Library.EntityFrameworkCore;
using Mes.Shared.Contracts.SharedKernel.Abstractions;
using Mes.Shared.Contracts.SharedKernel.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Mes.Hub.Edge.Synchronization.MasterData.Application;

internal sealed class MasterDataProvider(
    MasterDataTypeResolver typeResolver,
    DbContext context) : IMasterDataProvider
{
    private MethodInfo? _getEntities;

    public async Task<Dictionary<string, IMasterData[]>> GetAsync(string requestShopfloorKey, string[] masterDataTypes, int? page, int? pageSize, DateTime? requestLastUpdatedAt, CancellationToken cancellationToken)
    {
        var masterData = masterDataTypes.ToDictionary(type => type, _ => Array.Empty<IMasterData>());
        var getEntitiesMethodInfo = GetEntitiesMethodInfo();

        foreach (var entityType in masterDataTypes.Select(typeResolver.ResolveType).WhereNotNull())
        {
            var getEntities = getEntitiesMethodInfo.MakeGenericMethod(entityType);
            var entities = await InvokeGetEntities(getEntities, page, pageSize, requestLastUpdatedAt, cancellationToken);

            masterData[getEntities.Name] = entities;
        }

        return masterData;
    }

    private MethodInfo GetEntitiesMethodInfo()
    {
        if (_getEntities != null)
            return _getEntities;

        return _getEntities = GetType().GetMethod(nameof(GetEntites)) ?? throw new InvalidOperationException($"Could not find method '{nameof(GetEntites)}'.");
    }

    private async Task<IMasterData[]> InvokeGetEntities(MethodInfo getEntities, int? page, int? pageSize, DateTime? lastUpdatedAt, CancellationToken cancellationToken)
    {
        if (getEntities.Invoke(this, [page, pageSize, lastUpdatedAt, cancellationToken]) is not Task<IMasterData[]> task)
            return [];

        return await task;
    }

    private Task<TEntity[]> GetEntites<TEntity>(int? page, int? pageSize, DateTime? lastUpdatedAt, CancellationToken cancellationToken)
        where TEntity : class, IMasterData
    {
        var query = context
            .Set<TEntity>()
            .AsNoTracking() // Disables tracking for performance since we won't use those entities afterward anyway
            .IncludeRecursively(); // Includes all related entities from the include-path-string cache

        if (lastUpdatedAt.HasValue)
            query = query.Where(e => e.UpdatedAt >= lastUpdatedAt.Value); // UpdatedAt and CreatedAt are expected to be the same if the entity has been created after lastUpdatedAt

        if (page.HasValue && pageSize.HasValue)
            query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

        return query.ToArrayAsync(cancellationToken);
    }
}