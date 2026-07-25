using System.Collections.Concurrent;
using System.Reflection;

namespace Mes.Library.RabbitMQ.Producer;

public static class MessageRouteResolver
{
    private static readonly ConcurrentDictionary<Type, string[]> _messageRoutes = [];

    public static string[] ResolveRoutes(Type messageType)
    {
        return _messageRoutes.GetOrAdd(messageType, t =>
        {
            return t.GetCustomAttributes<MessageRouteAttribute>().Select(routeAttribute => routeAttribute.RoutingKey).ToArray();
        });
    }
}