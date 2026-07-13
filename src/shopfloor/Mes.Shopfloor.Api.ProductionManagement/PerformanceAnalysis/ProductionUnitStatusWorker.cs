using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Durational;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis;

internal sealed class ProductionUnitStatusWorker : IDurational
{
    public required Guid ProductionUnitId { get; init; }
    public required Guid WorkerId { get; init; }
    public required DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
}