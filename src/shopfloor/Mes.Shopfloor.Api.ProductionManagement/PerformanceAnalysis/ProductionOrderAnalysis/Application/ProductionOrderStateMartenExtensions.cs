using Marten;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Application;

internal static class ProductionOrderStateMartenExtensions
{
    public static Task<ProductionOrderStatus?> GetProductionOrderStatusByIdAsync(this IQuerySession session, Guid id, CancellationToken cancellationToken)
    {
        return session.Events.AggregateStreamAsync<ProductionOrderStatus>(id, token: cancellationToken);
    }
}