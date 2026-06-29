namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Contracts;

internal sealed class WorkerDto
{
    public Guid Id { get; init; }
    public string? Number { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required Guid GroupId { get; set; }
    public WorkerGroupDto? Group { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}