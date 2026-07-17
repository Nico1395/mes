using Marten;

namespace Mes.Shopfloor.Api.SharedKernel.Infrastructure.Persistence.Marten;

public interface IEventStoreConfiguration
{
    void Configure(StoreOptions storeOptions);
}