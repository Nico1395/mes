namespace Mes.Library.ShopfloorCommands.Connection;

public sealed class ShopfloorCommandHubConnectionReceiverConfigurationBuilder
{
    private readonly ShopfloorCommandHubConnectionReceiverConfiguration _configuration = new();

    public ShopfloorCommandHubConnectionReceiverConfigurationBuilder WithShopfloorKey(string shopfloorKey)
    {
        _configuration.ShopfloorKey = shopfloorKey;
        return this;
    }

    public ShopfloorCommandHubConnectionReceiverConfigurationBuilder WithHubBaseUrl(string baseUrl)
    {
        _configuration.HubBaseUrl = baseUrl;
        return this;
    }

    internal ShopfloorCommandHubConnectionReceiverConfiguration Build()
    {
        return _configuration;
    }
}