namespace Mes.Shopfloor.Core.Messaging;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public sealed class MessageRouteAttribute(string routingKey) : Attribute
{
    public string RoutingKey { get; } = routingKey;
}