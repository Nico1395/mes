using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;

internal static class StateEfCoreExtensions
{
    public static Task<State?> GetStateByIdAsync(this DbContext context, Guid id, CancellationToken cancellationToken)
    {
        return context.Set<State>().SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
    }
}