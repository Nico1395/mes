namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources.Contracts;

internal sealed record WorkerGroupQualificationDto
{
    public required Guid WorkerGroupId { get; init; }
    public WorkerGroupDto? WorkerGroup { get; init; }
    public required Guid WorkerQualificationId { get; init; }
    public WorkerQualificationDto? WorkerQualification { get; init; }
}