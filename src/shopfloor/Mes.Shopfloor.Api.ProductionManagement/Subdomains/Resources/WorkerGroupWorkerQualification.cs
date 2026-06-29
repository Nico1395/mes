namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources;

internal sealed record WorkerGroupWorkerQualification
{
    public required Guid WorkerGroupId { get; init; }
    public WorkerGroup? WorkerGroup { get; init; }
    public required Guid WorkerQualificationId { get; init; }
    public WorkerQualification? WorkerQualification { get; init; }
}