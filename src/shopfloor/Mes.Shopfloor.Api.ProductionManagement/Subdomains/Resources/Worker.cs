namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources;

internal sealed class Worker
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Number { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Guid GroupId { get; set; }
    public WorkerGroup? Group { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}