using System.Reflection;

namespace Mes.Library.ShopfloorCommands.Receiver;

public sealed class ShopfloorCommandReceiverConfiguration
{
    public Assembly[] Assemblies { get; set; } = [];
    public string? ShopfloorKey { get; set; }
    public string? HubBaseUrl { get; set; }
}