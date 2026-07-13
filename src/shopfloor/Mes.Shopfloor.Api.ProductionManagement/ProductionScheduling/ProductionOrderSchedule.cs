using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Timestamped;

namespace Mes.Shopfloor.Api.ProductionManagement.ProductionScheduling;

internal sealed class ProductionOrderSchedule : ITimestamped
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required Guid ProductionOrderId { get; set; }
    public required DateTime ScheduledToStartAt { get; set; }
    public required DateTime ScheduledToCompleteAt { get; set; }
    public List<ProductionOrderScheduleTask>? Tasks { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}