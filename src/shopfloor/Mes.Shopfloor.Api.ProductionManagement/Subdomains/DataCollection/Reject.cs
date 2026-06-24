namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.DataCollection;

internal sealed class Reject
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid GroupId { get; init; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Color { get; init; }
}