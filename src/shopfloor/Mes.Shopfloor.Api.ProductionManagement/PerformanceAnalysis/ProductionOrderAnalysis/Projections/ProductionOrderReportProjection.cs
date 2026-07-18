using Marten.Events.Aggregation;

namespace Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Projections;

public partial class ProductionOrderReportProjection : SingleStreamProjection<ProductionOrderReport, Guid>
{
}