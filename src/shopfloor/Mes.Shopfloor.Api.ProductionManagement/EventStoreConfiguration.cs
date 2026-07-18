using JasperFx.Events.Projections;
using Marten;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.ProductionOrderAnalysis.Projections;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence.Marten;

namespace Mes.Shopfloor.Api.ProductionManagement;

internal sealed class EventStoreConfiguration : IEventStoreConfiguration
{
    public void Configure(StoreOptions storeOptions)
    {
        storeOptions.Projections.Add<ProductionOrderReportProjection>(ProjectionLifecycle.Async);
        storeOptions.Schema.For<ProductionOrderReport>().Identity(p => p.ProductionOrderId);

        storeOptions.Projections.Add<ProductionOrderLiveStatusProjection>(ProjectionLifecycle.Async);
        storeOptions.Schema.For<ProductionOrderLiveStatus>().Identity(p => p.ProductionOrderId);
    }
}