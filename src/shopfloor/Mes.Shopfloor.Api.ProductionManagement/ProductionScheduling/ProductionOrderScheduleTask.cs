using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Durational;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ProductionOrderScheduleTask : IDurational
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid ProductionOrderScheduleId { get; set; }
    public required Guid ProductionUnitId { get; set; }
    public List<ProductionOrderScheduleTaskWorker>? Workers { get; set; }
    public required DateTime StartedAt { get; set; }
    public required DateTime? EndedAt { get; set; }
}