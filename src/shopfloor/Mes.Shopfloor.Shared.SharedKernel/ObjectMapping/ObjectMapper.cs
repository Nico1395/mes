using MapsterMapper;

namespace Mes.Shopfloor.Shared.SharedKernel.ObjectMapping;

internal sealed class ObjectMapper(IMapper _mapper) : IObjectMapper
{
    public TDestination Map<TDestination>(object source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TSource, TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map(source, destination);
    }

    public object Map(object source, Type sourceType, Type destinationType)
    {
        return _mapper.Map(source, sourceType, destinationType);
    }

    public object Map(object source, object destination, Type sourceType, Type destinationType)
    {
        return _mapper.Map(source, destination, sourceType, destinationType);
    }
}