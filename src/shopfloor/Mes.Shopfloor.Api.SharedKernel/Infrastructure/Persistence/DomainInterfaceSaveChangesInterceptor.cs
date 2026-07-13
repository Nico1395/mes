using Mes.Shopfloor.Api.SharedKernel.Domain;
using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Timestamped;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence;

internal sealed class DomainInterfaceSaveChangesInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        InterceptInternal(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
    {
        InterceptInternal(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void InterceptInternal(DbContextEventData eventData)
    {
        if (eventData.Context == null)
            return;

        var changedEntries = eventData.Context.ChangeTracker.Entries().Where(e => e.State is EntityState.Added or EntityState.Modified);
        var now = DateTime.UtcNow;

        foreach (var entry in changedEntries)
        {
            TouchIfCreatable(entry, now);
            TouchIfUpdateable(entry, now);
        }
    }

    private static void TouchIfCreatable(EntityEntry entry, DateTime now)
    {
        if (entry is { Entity: ICreated created, State: EntityState.Added })
            created.TouchCreatedAt(now);
    }
    
    private static void TouchIfUpdateable(EntityEntry entry, DateTime now)
    {
        if (entry is { Entity: IUpdated updated, State: EntityState.Added or EntityState.Modified })
            updated.TouchUpdatedAt(now);
    }
}