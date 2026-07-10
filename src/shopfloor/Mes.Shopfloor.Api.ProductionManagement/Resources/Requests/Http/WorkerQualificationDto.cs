namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Requests.Http;

internal sealed class WorkerQualificationDto
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<WorkerGroupQualificationDto>? WorkerGroups { get; set; }
    public List<ProductionUnitGroupQualificationDto>? ProductionUnitGroups { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}