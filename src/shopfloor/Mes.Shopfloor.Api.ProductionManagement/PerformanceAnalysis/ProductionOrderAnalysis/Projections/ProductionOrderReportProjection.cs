using Marten;
using Marten.Events.Aggregation;
using Mes.Shared.Contracts.SharedKernel.ProductionData.Events.Orders;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Application;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Projections;

public partial class ProductionOrderReportProjection : SingleStreamProjection<ProductionOrderReport, Guid>
{
    public async Task<ProductionOrderReport> Create(IQuerySession session, OrderCompletedV1 orderCompleted)
    {
        var status = await session.GetProductionOrderStatusByIdAsync(orderCompleted.ProductionOrderId, CancellationToken.None);
        if (status == null)
            throw new InvalidOperationException($"No status for production order '{orderCompleted.ProductionOrderId}' found. Cannot create a report.");

        return new ProductionOrderReport
        {
            ProductionOrderId = orderCompleted.ProductionOrderId,
            ScheduledProductionOrderId = status.ScheduledProductionOrderId,
            ProductId = status.ProductId,
            TargetQuantity = status.TargetQuantity,
            ProducedQuantity = status.ProducedQuantity,
            CompletionPercent = status.ProgressPercent,
            ProducedRejectQuantity = status.ProducedRejectQuantity,
            ScheduledToStartAt = status.ScheduledToStartAt,
            ScheduledToCompleteAt = status.ScheduledToCompleteAt,
            StartedAt = orderCompleted.StartedAt,
            CompletedAt = orderCompleted.CompletedAt,
        };
    }
}