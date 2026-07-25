using Mapster;

namespace Mes.Library.ObjectMapping;

public static class ObjectExtensions
{
    public static TDestination Map<TDestination>(this object item)
    {
        return item.Adapt<TDestination>();
    }
}