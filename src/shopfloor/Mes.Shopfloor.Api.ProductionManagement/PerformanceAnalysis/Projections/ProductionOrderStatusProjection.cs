using DandyMediator;
using Marten.Events.Aggregation;
using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling.Application;
using Mes.Shopfloor.Api.SharedKernel.Domain.Events;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Microsoft.EntityFrameworkCore;
 
namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Projections;

internal partial class ProductionOrderStatusProjection(
    IMediator mediator,
    DbContext context) : SingleStreamProjection<ProductionOrderStatus, Guid>
{
    public async Task<ProductionOrderStatus> Create(ProductionOrderScheduledV1 orderScheduled)
    {
        var productionOrder = await context.GetProductionOrderByIdAsync(orderScheduled.ProductionOrderId, cancellationToken: CancellationToken.None);
        if (productionOrder == null)
            throw new InvalidOperationException($"Production order '{orderScheduled.ProductionOrderId}' not found.");

        var productionOrderSchedule = await context.GetProductionOrderScheduleByOrderIdEagerAsync(orderScheduled.ProductionOrderId, CancellationToken.None);
        if (productionOrderSchedule == null)
            throw new InvalidOperationException($"No schedule for production order '{orderScheduled.ProductionOrderId}' found.");

        return new ProductionOrderStatus
        {
            ProductionOrderId = productionOrder.Id,
            ScheduledProductionOrderId = productionOrderSchedule.Id,
            ProductId = productionOrder.ProductId,
            Priority = productionOrder.Priority,
            Name = productionOrder.Name,
            TargetQuantity = productionOrder.TargetQuantity,
            ScheduledToStartAt = productionOrderSchedule.ScheduledToStartAt,
            ScheduledToCompleteAt = productionOrderSchedule.ScheduledToCompleteAt,
        };
    }

    public void Apply(OrderBookedV1 orderBooked, ProductionOrderStatus status)
    {
        if (orderBooked.ProductionOrderId == status.ProductionOrderId) // Order was just booked
        {
            var notCompletedAndBooked = status.TryBook(orderBooked.ProductionUnitId, orderBooked.ScheduledTaskId, orderBooked.OccurredAtUtc);
            if (!notCompletedAndBooked)
            {
                // TODO -> Send some information message to some notification center
            }
        }
        else if (orderBooked.PreviousProductionOrderId == status.ProductionOrderId) // Order was 'unbooked'
        {
            status.SetNotBooked();
        }
    }

    public async Task Apply(QuantityProducedV1 quantityProduced, ProductionOrderStatus status)
    {
        var alreadyCompleted = status.IsAbortedOrCompleted();

        status.AddProducedQuantity(
            quantityProduced.ProductionUnitId,
            quantityProduced.ProducedQuantity,
            quantityProduced.OccurredAtUtc);

        // Order has just been completed
        if (!alreadyCompleted && status.IsAbortedOrCompleted())
        {
            var orderCompleted = new OrderCompletedV1(
                status.ProductionOrderId,
                status.ScheduledToStartAt,
                status.ScheduledToCompleteAt,
                status.StartedAt.Value,
                status.CompletedAt.Value,
                status.TargetQuantity,
                status.ProducedQuantity,
                status.ProducedRejectQuantity);
            await mediator.PublishAsync(orderCompleted);
        }
    }

    public void Apply(RejectQuantityProducedV1 rejectQuantityProduced, ProductionOrderStatus status)
    {
        var producedReject = new ProductionOrderProducedReject
        {
            ProductionOrderId = status.ProductionOrderId,
            ProductionUnitId = rejectQuantityProduced.ProductionUnitId,
            Quantity = rejectQuantityProduced.ProducedRejectQuantity,
            RejectId = rejectQuantityProduced.RejectId,
            ReportedAt = rejectQuantityProduced.OccurredAtUtc,
        };

        status.ProducedRejectQuantities.Add(producedReject);
        status.ProducedRejectQuantity += producedReject.Quantity;
        status.Touch();
    }

    public void Apply(MaterialConsumedV1 materialConsumed, ProductionOrderStatus status)
    {
        var materialConsumption = new ProductionOrderMaterialConsumption
        {
            ProductionOrderId = status.ProductionOrderId,
            ProductionUnitId = materialConsumed.ProductionUnitId,
            MaterialId = materialConsumed.MaterialId,
            Quantity = materialConsumed.Quantity,
            ReportedAt = materialConsumed.OccurredAtUtc,
        }
            ;

        status.MaterialConsumption.Add(materialConsumption);
        status.Touch();
    }

    public void Apply(PartsConsumedV1 partsConsumed, ProductionOrderStatus status)
    {
        var partsConsumption = new ProductionOrderPartsConsumption
        {
            ProductionOrderId = status.ProductionOrderId,
            ProductionUnitId = partsConsumed.ProductionUnitId,
            PartId = partsConsumed.PartId,
            Quantity = partsConsumed.Quantity,
            ReportedAt = partsConsumed.OccurredAtUtc,
        };

        status.PartConsumption.Add(partsConsumption);
        status.Touch();
    }
}