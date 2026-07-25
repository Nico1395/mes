namespace Mes.Library.ShopfloorCommands.Sender;

public sealed class ShopfloorCommandSenderConfigurationBuilder
{
    private readonly ShopfloorCommandSenderConfiguration _configuration = new();

    public ShopfloorCommandSenderConfigurationBuilder WithRedisUrl(string redisUrl)
    {
        _configuration.RedisUrl = redisUrl;
        return this;
    }
    
    internal ShopfloorCommandSenderConfiguration Build()
    {
        return _configuration;
    }
}