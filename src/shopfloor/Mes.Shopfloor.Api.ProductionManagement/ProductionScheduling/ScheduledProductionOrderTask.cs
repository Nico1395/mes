using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Durational;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledProductionOrderTask : IDurational
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid ScheduledProductionOrderId { get; set; }
    public required Guid ProductionProcessStepId { get; set; }
    public required Guid ProductionUnitId { get; set; }
    public List<ScheduledProductionOrderTaskWorker>? Workers { get; set; }
    public required DateTime StartedAt { get; set; }
    public required DateTime? EndedAt { get; set; }
}