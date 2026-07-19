namespace Mes.Shopfloor.Client.SharedKernel.Configuration;

public sealed class MessageBrokerOptions
{
    public string[]? Nodes { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}