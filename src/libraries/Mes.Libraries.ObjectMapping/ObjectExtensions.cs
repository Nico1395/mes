using Mapster;

namespace Mes.Libraries.ObjectMapping;

public static class ObjectExtensions
{
    public static TDestination Map<TDestination>(this object item)
    {
        return item.Adapt<TDestination>();
    }
}