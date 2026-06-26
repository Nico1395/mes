namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Contracts;

internal sealed class RejectGroupDto
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<RejectDto>? Rejects { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}