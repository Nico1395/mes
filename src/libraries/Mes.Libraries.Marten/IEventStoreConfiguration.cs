using Marten;

namespace Mes.Libraries.Marten;

public interface IEventStoreConfiguration
{
    void Configure(StoreOptions storeOptions);
}