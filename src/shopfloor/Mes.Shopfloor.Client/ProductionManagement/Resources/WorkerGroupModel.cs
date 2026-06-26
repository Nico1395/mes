namespace Mes.Shopfloor.Client.ProductionManagement.Resources;

internal sealed class WorkerGroupModel
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required List<WorkerQualificationModel> Qualifications { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}