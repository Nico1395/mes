namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources;

internal sealed class WorkerQualification
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<WorkerGroupWorkerQualification>? WorkerGroups { get; set; }
    public List<ProductionUnitGroupQualification>? ProductionUnitGroups { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}