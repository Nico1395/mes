using JasperFx.Events.Projections;
using Marten;
using Mes.Libraries.Marten;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Projections;

namespace Mes.Shopfloor.Api.ProductionManagement;

internal sealed class EventStoreConfiguration : IEventStoreConfiguration
{
    public void Configure(StoreOptions storeOptions)
    {
        storeOptions.Projections.Add<ProductionOrderReportProjection>(ProjectionLifecycle.Async);
        storeOptions.Schema.For<ProductionOrderReport>().Identity(p => p.ProductionOrderId);
    }
}