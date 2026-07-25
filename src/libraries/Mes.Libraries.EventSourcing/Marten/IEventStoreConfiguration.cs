using Marten;

namespace Mes.Libraries.EventSourcing.Marten;

public interface IEventStoreConfiguration
{
    void Configure(StoreOptions storeOptions);
}