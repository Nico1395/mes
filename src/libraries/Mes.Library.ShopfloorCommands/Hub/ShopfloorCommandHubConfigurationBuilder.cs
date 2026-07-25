namespace Mes.Library.ShopfloorCommands.Hub;

public sealed class ShopfloorCommandHubConfigurationBuilder
{
    private readonly ShopfloorCommandHubConfiguration _configuration = new();

    public ShopfloorCommandHubConfigurationBuilder WithRedisUrl(string redisUrl)
    {
        _configuration.RedisUrl = redisUrl;
        return this;
    }

    internal ShopfloorCommandHubConfiguration Build()
    {
        return _configuration;
    }
}