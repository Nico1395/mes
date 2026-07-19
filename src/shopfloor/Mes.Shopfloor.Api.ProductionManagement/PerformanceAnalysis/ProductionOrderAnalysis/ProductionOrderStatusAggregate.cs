using Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;
using Mes.Shopfloor.Api.SharedKernel.Domain.Exceptions;
using Mes.Shopfloor.Shared.SharedKernel.Events;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis;

internal sealed class ProductionOrderStatusAggregate : ProductionOrderStatus
{
    public static ProductionOrderStatus Create(OrderScheduledV1 orderScheduled, ProductionOrder productionOrder, ScheduledProductionOrder scheduledProductionOrder)
    {
        return new ProductionOrderStatus
        {
            ProductionOrderId = productionOrder.Id,
            ScheduledProductionOrderId = scheduledProductionOrder.Id,
            ProductId = productionOrder.ProductId,
            Name = productionOrder.Name,
            Priority = productionOrder.Priority,
            TargetQuantity = productionOrder.TargetQuantity,
            ScheduledToStartAt = scheduledProductionOrder.ScheduledToStartAt,
            ScheduledToCompleteAt = scheduledProductionOrder.ScheduledToCompleteAt,
        };
    }

    public OrderBookedResult Apply(OrderBookedV1 orderBooked)
    {
        if (orderBooked.ProductionOrderId == ProductionOrderId) // Order was just booked
        {
            var abortedOrCompleted = IsAbortedOrCompleted();
            StartOrResume();

            CurrentProductionUnitId = orderBooked.ProductionUnitId;
            CurrentScheduledTaskId = orderBooked.ScheduledTaskId;

            var booking = new ProductionOrderBooking()
            {
                ProductionUnitId = orderBooked.ProductionUnitId,
                ScheduledTaskId = orderBooked.ScheduledTaskId,
                BookedAt = orderBooked.OccurredAtUtc,           // TODO -> Implement IDurational
            };
            Bookings.Add(booking);

            Touch();
            return abortedOrCompleted ? OrderBookedResult.BookedButAbortedOrCompleted : OrderBookedResult.Booked;
        }
        else if (orderBooked.PreviousProductionOrderId == ProductionOrderId) // Order was 'unbooked'
        {
            SetNotBooked();
            return OrderBookedResult.Unbooked;
        }

        throw InvalidMessageException.Create<OrderBookedV1>();
    }

    public bool Apply(QuantityProducedV1 quantityProduced)
    {
        var alreadyCompleted = IsAbortedOrCompleted();

        AddProducedQuantity(
            quantityProduced.ProductionUnitId,
            quantityProduced.ProducedQuantity,
            quantityProduced.OccurredAtUtc);

        var hasBeenCompleted = !alreadyCompleted && IsAbortedOrCompleted();
        return hasBeenCompleted;
    }

    public void Apply(RejectQuantityProducedV1 rejectQuantityProduced)
    {
        var producedReject = new ProductionOrderProducedReject
        {
            ProductionOrderId = ProductionOrderId,
            ProductionUnitId = rejectQuantityProduced.ProductionUnitId,
            Quantity = rejectQuantityProduced.ProducedRejectQuantity,
            RejectId = rejectQuantityProduced.RejectId,
            ReportedAt = rejectQuantityProduced.OccurredAtUtc,
        };

        ProducedRejectQuantities.Add(producedReject);
        ProducedRejectQuantity += producedReject.Quantity;
        Touch();
    }

    public void Apply(MaterialConsumedV1 materialConsumed)
    {
        var materialConsumption = new ProductionOrderMaterialConsumption
        {
            ProductionOrderId = ProductionOrderId,
            ProductionUnitId = materialConsumed.ProductionUnitId,
            MaterialId = materialConsumed.MaterialId,
            Quantity = materialConsumed.Quantity,
            ReportedAt = materialConsumed.OccurredAtUtc,
        };

        MaterialConsumption.Add(materialConsumption);
        Touch();
    }

    public void Apply(PartsConsumedV1 partsConsumed)
    {
        var partsConsumption = new ProductionOrderPartsConsumption
        {
            ProductionOrderId = ProductionOrderId,
            ProductionUnitId = partsConsumed.ProductionUnitId,
            PartId = partsConsumed.PartId,
            Quantity = partsConsumed.Quantity,
            ReportedAt = partsConsumed.OccurredAtUtc,
        };

        PartConsumption.Add(partsConsumption);
        Touch();
    }
}