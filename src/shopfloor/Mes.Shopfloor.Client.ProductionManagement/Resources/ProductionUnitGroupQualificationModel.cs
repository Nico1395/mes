namespace Mes.Shopfloor.Client.ProductionManagement.Resources;

internal sealed class ProductionUnitGroupQualificationModel
{
    public required int ProductionUnitGroupId { get; set; }
    public ProductionUnitGroupModel? ProductionUnitGroup { get; set; }
    public required Guid WorkerQualificationId { get; set; }
    public WorkerQualificationModel? WorkerQualification { get; set; }
}