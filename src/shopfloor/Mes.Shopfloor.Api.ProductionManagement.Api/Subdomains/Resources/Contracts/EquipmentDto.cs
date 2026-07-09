namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources.Contracts;

internal sealed class EquipmentDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}