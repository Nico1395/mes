namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection.Contracts;

internal sealed class StateGroupDto
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<StateDto>? States { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}