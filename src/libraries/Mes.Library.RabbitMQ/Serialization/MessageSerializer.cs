using System.Text.Json;

namespace Mes.Library.RabbitMQ.Serialization;

public static class MessageSerializer
{
    public static string Serialize(object @event)
    {
        return JsonSerializer.Serialize(@event);
    }

    public static object? Deserialize(Type type, string json)
    {
        return JsonSerializer.Deserialize(json, type);
    }
}