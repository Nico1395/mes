namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources;

internal sealed class WorkerQualification
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid GroupId { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}