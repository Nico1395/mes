namespace Mes.Shopfloor.Client.ProductionManagement.ResourceManagement;

internal sealed record WorkerGroupQualificationModel
{
    public required Guid WorkerGroupId { get; init; }
    public WorkerGroupModel? WorkerGroup { get; init; }
    public required Guid WorkerQualificationId { get; init; }
    public WorkerQualificationModel? WorkerQualification { get; init; }
}