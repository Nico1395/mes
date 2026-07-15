using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Timestamped;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ScheduledProductionOrder : ITimestamped
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid ProductionOrderId { get; set; }
    public Guid? ProductionProcessId { get; set; }
    public required DateTime ScheduledToStartAt { get; set; }
    public required DateTime ScheduledToCompleteAt { get; set; }
    public List<ScheduledProductionOrderTask>? Tasks { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}