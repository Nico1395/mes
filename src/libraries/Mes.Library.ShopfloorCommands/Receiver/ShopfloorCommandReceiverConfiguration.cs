using System.Reflection;

namespace Mes.Library.ShopfloorCommands.Receiver;

public sealed class ShopfloorCommandReceiverConfiguration
{
    public Assembly[] Assemblies { get; set; } = [];
}