namespace Mes.Shopfloor.Api.ProductionManagement.Subdomains.Resources;

internal sealed class ProductionUnitGroupQualification
{
    public required int ProductionUnitGroupId { get; set; }
    public ProductionUnitGroup? ProductionUnitGroup { get; set; }
    public required Guid WorkerQualificationId { get; set; }
    public WorkerQualification? WorkerQualification { get; set; }
}