namespace Mes.Shopfloor.Api.ProductionManagement.ResourceManagement.Requests.Http;

internal sealed class WorkerGroupDto
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<WorkerGroupQualificationDto>? Qualifications { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}