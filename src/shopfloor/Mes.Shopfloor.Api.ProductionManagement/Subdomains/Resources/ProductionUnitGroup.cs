namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources;

internal sealed class ProductionUnitGroup
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Guid StateGroupId { get; set; }
    public required Guid RejectGroupId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}