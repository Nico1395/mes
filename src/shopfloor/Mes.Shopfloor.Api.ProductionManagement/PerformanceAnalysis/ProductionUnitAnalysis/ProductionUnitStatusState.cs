using Mes.Library.Domain.Abstractions.Durational;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionUnitAnalysis;

internal sealed class ProductionUnitStatusState : IDurational
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid ProductionUnitId { get; init; }
    public required Guid StateId { get; init; }
    public required bool IsProductive { get; init; }
    public required bool IsIdle { get; init; }
    public required DateTime StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
}