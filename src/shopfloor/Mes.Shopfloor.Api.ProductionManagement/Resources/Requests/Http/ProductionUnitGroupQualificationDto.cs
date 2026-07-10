namespace Mes.Shopfloor.Api.ProductionManagement.Resources.Requests.Http;

internal sealed class ProductionUnitGroupQualificationDto
{
    public required int ProductionUnitGroupId { get; set; }
    public ProductionUnitGroupDto? ProductionUnitGroup { get; set; }
    public required Guid WorkerQualificationId { get; set; }
    public WorkerQualificationDto? WorkerQualification { get; set; }
}