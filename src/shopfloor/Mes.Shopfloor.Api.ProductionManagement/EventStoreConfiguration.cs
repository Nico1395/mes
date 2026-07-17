using JasperFx.Events.Projections;
using Marten;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis;
using Mes.Shopfloor.Api.ProductionManagement.PerformanceAnalysis.Projections;
using Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence.Marten;

namespace Mes.Shopfloor.Api.ProductionManagement;

internal sealed class EventStoreConfiguration : IEventStoreConfiguration
{
    public void Configure(StoreOptions storeOptions)
    {
        storeOptions.Projections.Add<ProductionUnitStatusProjection>(ProjectionLifecycle.Async);
        storeOptions.Schema.For<ProductionUnitStatus>().Identity(p => p.ProductionUnitId);
        
        storeOptions.Projections.Add<ProductionOrderStatusProjection>(ProjectionLifecycle.Async);
        storeOptions.Schema.For<ProductionOrderStatus>().Identity(p => p.ProductionOrderId);
    }
}