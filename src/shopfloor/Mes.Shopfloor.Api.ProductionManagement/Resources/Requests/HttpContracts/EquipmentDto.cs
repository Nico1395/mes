namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Requests.HttpContracts;

internal sealed class EquipmentDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}