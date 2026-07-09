namespace Mes.Shopfloor.Api.ProductionManagement.Api.Subdomains.Resources.Contracts;

internal sealed class ProductionUnitGroupQualificationDto
{
    public required int ProductionUnitGroupId { get; set; }
    public ProductionUnitGroupDto? ProductionUnitGroup { get; set; }
    public required Guid WorkerQualificationId { get; set; }
    public WorkerQualificationDto? WorkerQualification { get; set; }
}