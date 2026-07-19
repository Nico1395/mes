namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement;

internal sealed class WorkerGroupModel
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<WorkerGroupQualificationModel>? Qualifications { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}