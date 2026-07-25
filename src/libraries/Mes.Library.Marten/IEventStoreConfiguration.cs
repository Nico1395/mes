using Marten;

namespace Mes.Library.Marten;

public interface IEventStoreConfiguration
{
    void Configure(StoreOptions storeOptions);
}