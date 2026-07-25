using System.Reflection;

namespace Mes.Library.ShopfloorCommands.Receiver;

public sealed class ShopfloorCommandReceiverConfigurationBuilder
{
    private readonly ShopfloorCommandReceiverConfiguration _configuration = new();

    public ShopfloorCommandReceiverConfigurationBuilder ScanInAssemblies(params Assembly[] assemblies)
    {
        _configuration.Assemblies = assemblies;
        return this;
    }

    internal ShopfloorCommandReceiverConfiguration Build()
    {
        return _configuration;
    }
}