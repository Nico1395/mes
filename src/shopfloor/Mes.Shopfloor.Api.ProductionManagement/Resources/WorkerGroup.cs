namespace Mes.Shopfloor.Api.ProductionManagement.Resources;

internal sealed class WorkerGroup
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<WorkerGroupWorkerQualification>? Qualifications { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}