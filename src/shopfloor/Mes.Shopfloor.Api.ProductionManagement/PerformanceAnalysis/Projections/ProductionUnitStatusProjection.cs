using Marten.Events.Aggregation;
using Mes.Shopfloor.Api.ProductionManagement.DataCollection.Application;
using Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Application;
using Mes.Shopfloor.Shared.SharedKernel.Events;
using Microsoft.EntityFrameworkCore;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Projections;

internal partial class ProductionUnitStatusProjection(DbContext context) : SingleStreamProjection<ProductionUnitStatus, Guid>
{
    public async Task<ProductionUnitStatus> Create(ProductionUnitWentOnlineV1 wentOnline)
    {
        var exists = await context.ProductionUnitExistsAsync(wentOnline.ProductionUnitId, CancellationToken.None);
        if (exists)
            throw new InvalidOperationException();

        var status = new ProductionUnitStatus
        {
            ProductionUnitId = wentOnline.ProductionUnitId,
        };

        status.TrySetWorker(wentOnline.WorkerId, wentOnline.OccurredAtUtc);
        return status;
    }

    public void Apply(OrderBookedV1 orderBooked, ProductionUnitStatus status)
    {
        status.SetOrder(
            orderBooked.ProductionOrderId,
            orderBooked.ProductionStepId,
            orderBooked.ScheduledTaskId);
        
        // Should the state be set? No because the production unit's state being set is dealt with using another event.
    }

    public async Task Apply(ProductionUnitStateChangedV1 stateChanged, ProductionUnitStatus status)
    {
        var state = await context.GetProductionUnitStateByIdAsync(stateChanged.NewStateId, CancellationToken.None);
        if (state == null)
            throw new InvalidOperationException($"State with ID '{stateChanged.NewStateId}' not found.");

        status.SetState(
            state.Id,
            state.IsProductive,
            state.IsIdle,
            stateChanged.OccurredAtUtc);
    }

    public void Apply(QuantityProducedV1 quantityProduced, ProductionUnitStatus status)
    {
        var producedQuantity = new ProductionUnitProducedQuantity
        {
            ProductionOrderId = quantityProduced.ProductionOrderId,
            ProductionUnitId = quantityProduced.ProductionUnitId,
            Quantity = quantityProduced.ProducedQuantity,
            ReportedAt = quantityProduced.OccurredAtUtc,
        };

        status.ProducedQuantities.Add(producedQuantity);
        status.ProductionOrderId = quantityProduced.ProductionOrderId;

        status.Touch();
    }

    public void Apply(RejectQuantityProducedV1 rejectQuantityProduced, ProductionUnitStatus status)
    {
        var producedReject = new ProductionUnitProducedReject
        {
            ProductionOrderId = rejectQuantityProduced.ProductionOrderId,
            ProductionUnitId = rejectQuantityProduced.ProductionUnitId,
            Quantity = rejectQuantityProduced.ProducedRejectQuantity,
            RejectId = rejectQuantityProduced.RejectId,
            ReportedAt = rejectQuantityProduced.OccurredAtUtc,
        };

        status.ProducedRejectQuantities.Add(producedReject);
        status.ProductionOrderId = rejectQuantityProduced.ProductionOrderId;

        status.Touch();
    }

    public void Apply(MaterialConsumedV1 materialConsumed, ProductionUnitStatus status)
    {
        var materialConsumption = new ProductionUnitMaterialConsumption
        {
            ProductionOrderId = materialConsumed.ProductionOrderId,
            ProductionUnitId = materialConsumed.ProductionUnitId,
            MaterialId = materialConsumed.MaterialId,
            Quantity = materialConsumed.Quantity,
            ReportedAt = materialConsumed.OccurredAtUtc,
        };

        status.MaterialConsumption.Add(materialConsumption);
        status.ProductionOrderId = materialConsumed.ProductionOrderId;

        status.Touch();
    }

    public void Apply(PartsConsumedV1 partsConsumed, ProductionUnitStatus status)
    {
        var partsConsumption = new ProductionUnitPartsConsumption
        {
            ProductionOrderId = partsConsumed.ProductionOrderId,
            ProductionUnitId = partsConsumed.ProductionUnitId,
            PartId = partsConsumed.PartId,
            Quantity = partsConsumed.Quantity,
            ReportedAt = partsConsumed.OccurredAtUtc,
        };

        status.PartConsumption.Add(partsConsumption);
        status.ProductionOrderId = partsConsumed.ProductionOrderId;

        status.Touch();
    }
}