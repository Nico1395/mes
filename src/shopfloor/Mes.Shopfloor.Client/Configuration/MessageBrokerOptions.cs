namespace Mes.Shopfloor.Client.Configuration;

public sealed class MessageBrokerOptions
{
    public string[]? Nodes { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}