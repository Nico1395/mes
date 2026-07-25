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

    public ShopfloorCommandReceiverConfigurationBuilder WithShopfloorKey(string shopfloorKey)
    {
        _configuration.ShopfloorKey = shopfloorKey;
        return this;
    }

    public ShopfloorCommandReceiverConfigurationBuilder WithHubBaseUrl(string baseUrl)
    {
        _configuration.HubBaseUrl = baseUrl;
        return this;
    }

    internal ShopfloorCommandReceiverConfiguration Build()
    {
        return _configuration;
    }
}